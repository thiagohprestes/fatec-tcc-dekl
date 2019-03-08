using AutoMapper;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DEKL.CP.Domain.Enums;
using DEKL.CP.UI.ViewModels.InternalBankAccount;
using DEKL.CP.UI.ViewModels.Provider;

namespace DEKL.CP.UI.Controllers
{
    [Authorize]
    public class AccountToPayController : Controller
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAccountToPayRepository _accountToPayRepository;
        private readonly IAccountToPayRepository _accountToPayRepositoryAdd;
        private readonly IInternalBankAccountRepository _internalBankAccountRepository;
        private readonly IProviderBankAccountRepository _providerBankAccountRepository;

        public AccountToPayController(IAccountToPayRepository accountToPayRepository,
                                        IAccountToPayRepository accountToPayRepositoryAdd,
                                      IProviderRepository providerRepository,
                                      IInternalBankAccountRepository internalBankAccountRepository,
                                      IProviderBankAccountRepository providerBankAccountRepository)
        {
            _providerRepository = providerRepository;
            _internalBankAccountRepository = internalBankAccountRepository;
            _providerBankAccountRepository = providerBankAccountRepository;
            _accountToPayRepository = accountToPayRepository;
            _accountToPayRepositoryAdd = accountToPayRepositoryAdd;

        }

        public ActionResult Index()
            => View(Mapper.Map<IEnumerable<AccountToPayRelashionships>>(_accountToPayRepository.AccountToPayActivesRelashionships));

        public ActionResult Create()
        {
            ViewBag.Providers = new SelectList(_providerRepository.AllActivesProviderPhysicalLegalPerson, nameof(Provider.Id),
                nameof(IProviderPhysicalLegalPerson.NameCorporateName));
            return View();
        }

        [HttpPost]
        public ActionResult Create(AccountToPay accountToPay)
        {
            if (ModelState.IsValid)
            {
                try
                {   // se é conta mensal não possuí parcelas
                    if (accountToPay.MonthlyAccount)
                    {
                        accountToPay.NumberOfInstallments = 0;
                    }

                    var valueInstallment = accountToPay.NumberOfInstallments == 0 ?
                        accountToPay.Value : accountToPay.Value / accountToPay.NumberOfInstallments;

                    // criação das parcelas                   
                    for (var i = 0; i < accountToPay.NumberOfInstallments; i++)
                    {
                        var installment = new Installment
                        {
                            MaturityDate = accountToPay.MaturityDate.AddMonths(i),
                            Value = valueInstallment,
                        };

                        accountToPay.Installments.Add(installment);
                    }

                    accountToPay.ApplicationUserId = User.Identity.GetUserId<int>();

                    _accountToPayRepository.Add(accountToPay);

                    this.AddToastMessage("Conta salva", $"A conta {accountToPay.Description} foi salva com sucesso", 
                                         ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro no salvamento", $"Erro ao salvar a conta {accountToPay.Description}, " +
                                         "favor tentar novamente", ToastType.Error);

                }
            }

            return Create();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(Mapper.Map<AccountToPayViewModel>(_accountToPayRepository.Find(id.Value)));
        }

        public ActionResult Payment(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            return View(Mapper.Map<AccountToPayViewModel>(_accountToPayRepository.Find(id.Value)));
        }

        public ActionResult InstallmentPayment(int? id, int installment_Id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var accountToPay = _accountToPayRepository.Find((int)id);

            ViewBag.Installment_Id = installment_Id;

            ViewBag.InternalBankAccounts = new SelectList(Mapper.Map<IEnumerable<InternalBankAccountRelashionshipsViewModel>>(_internalBankAccountRepository.InternalBankAccountRelashionships),
                                                          nameof(InternalBankAccountRelashionshipsViewModel.Id),
                                                          nameof(InternalBankAccountRelashionshipsViewModel.DescriptionAccount));

            ViewBag.ProviderBankAccounts = new SelectList(Mapper.Map<IEnumerable<ProviderBankAccountRelashionshipsViewModel>>(_providerBankAccountRepository.ProviderBankAccountRelashionships(accountToPay.ProviderId)),
                                                          nameof(ProviderBankAccountRelashionshipsViewModel.Id),
                                                          nameof(ProviderBankAccountRelashionshipsViewModel.DescriptionAccount));

            return View(Mapper.Map<AccountToPayViewModel>(_accountToPayRepository.Find(id.Value)));
        }

        public ActionResult PayAccount(int id, int installment_id, PaymentType paymentType, int? internalBankAccount_id,
            int? providerBankAccount_id)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            decimal amountDue;
            var accountToPay = _accountToPayRepository.FindActive(id);
            var hasInstallments = accountToPay.Installments.Any();

            //não possuí parcelas
            if (!hasInstallments)
            {
                //conta em atraso
                if (accountToPay.MaturityDate < DateTime.Now && accountToPay.PaidValue < accountToPay.Value)
                {
                    var daysPastDue = (int)DateTime.Now.Subtract(new DateTime(accountToPay.MaturityDate.Year,
                        accountToPay.MaturityDate.Month,
                        accountToPay.MaturityDate.Day)).TotalDays;

                    amountDue = accountToPay.Value - accountToPay.Installments
                                                                 .Where(i => i.PaidValue >= i.Value)
                                                                 .Sum(i => i.Value);

                    amountDue += amountDue * accountToPay.Penalty / 100;
                    amountDue += amountDue * daysPastDue * accountToPay.DailyInterest / 100;
                }
                //conta em dia
                else
                {
                    accountToPay.PaidValue = accountToPay.Value;
                }

                if (accountToPay.MonthlyAccount)
                {
                    var newAccountToPay = new AccountToPay
                    {
                        MaturityDate = accountToPay.MaturityDate.AddMonths(1),
                        ApplicationUserId = User.Identity.GetUserId<int>(),
                        DailyInterest = accountToPay.DailyInterest,
                        Description = accountToPay.Description,
                        DocumentNumber = accountToPay.DocumentNumber,
                        MonthlyAccount = accountToPay.MonthlyAccount,
                        NumberOfInstallments = accountToPay.NumberOfInstallments,
                        PaymentSimulators = accountToPay.PaymentSimulators,
                        Penalty = accountToPay.Penalty,
                        Priority = accountToPay.Priority,
                        ProviderId = accountToPay.ProviderId,
                        Value = accountToPay.Value
                    };

                    _accountToPayRepository.Add(newAccountToPay);

                }
                else
                {
                    //vai pagar a conta inteira
                    if (installment_id == 0)
                    {
                        //parcelas vencidas
                        var overdueInstallments = accountToPay.Installments
                            .Where(i => i.MaturityDate < DateTime.Now && i.PaidValue < i.Value)
                            .ToList();

                        //parcelas em dia e ainda não pagas
                        var installmentsOk = accountToPay.Installments
                            .Except(overdueInstallments)
                            .Where(i => i.PaidValue < i.Value)
                            .ToList();


                        //valor total pago sem juros e mora
                        var withoutDue = amountDue = accountToPay.Value - accountToPay.Installments
                                                         .Except(overdueInstallments)
                                                         .Except(installmentsOk)
                                                         .Sum(i => i.Value);

                        overdueInstallments.ForEach(o =>
                        {
                            var daysPastDue = (int)DateTime.Now.Subtract(new DateTime(o.MaturityDate.Year,
                                o.MaturityDate.Month,
                                o.MaturityDate.Day)).TotalDays;

                            amountDue += amountDue * accountToPay.Penalty / 100;
                            amountDue += amountDue * daysPastDue * accountToPay.DailyInterest / 100;
                        });

                        //parcelas em atraso
                        overdueInstallments.ForEach(i =>
                        {
                            i.PaymentDate = DateTime.Now;
                            i.PaidValue = (amountDue - withoutDue) / overdueInstallments.Count;
                        });

                        installmentsOk.ForEach(i =>
                        {
                            i.PaymentDate = DateTime.Now;
                            i.PaidValue = i.Value;
                        });

                        amountDue += installmentsOk.Sum(i => i.Value);
                    }

                    else
                    {
                        var installment = accountToPay.Installments.FirstOrDefault(i => i.Id == installment_id) ??
                                          new Installment();

                        var daysPastDue = (int)DateTime.Now.Subtract(new DateTime(installment.MaturityDate.Year,
                            installment.MaturityDate.Month,
                            installment.MaturityDate.Day)).TotalDays;
                        if (daysPastDue > 0)
                        {
                            installment.PaidValue += installment.Value * accountToPay.Penalty / 100;
                            installment.PaidValue +=
                                installment.PaidValue * daysPastDue * accountToPay.DailyInterest / 100;
                        }

                        amountDue = installment.PaidValue ?? 0;
                    }
                }

                //if (!hasInstallments)
                //{
                //    if (hasInstallments)
                //    {
                //        var diasVencidos = (int)DateTime.Now.Subtract(new DateTime(accountToPay.MaturityDate.Year, 
                //                                                                   accountToPay.MaturityDate.Month, 
                //                                                                   accountToPay.MaturityDate.Day)).TotalDays;

                //        var diaria = accountToPay.Value + accountToPay.Value * accountToPay.Penalty / 100;
                //        decimal? multaDiaria = diaria * diasVencidos * accountToPay.DailyInterest / 100;

                //        accountToPay.PaidValue = (diaria + multaDiaria).Value;

                //        // pagamento de conta sem estar vencida
                //        accountToPay.PaidValue = accountToPay.PaidValue;
                //        accountToPay.PaymentDate = DateTime.Now;
                //        accountToPay.PaymentType = paymentType;

                //        //pagamento parcelas
                //        foreach (var installment in accountToPay.Installments)
                //        {
                //            installment.PaymentDate = DateTime.Now;
                //            installment.PaidValue = installment.Value;
                //        }
                //    }
                //    else
                //    {
                //        var parcelaSelecionada =  accountToPay.Installments.ToList().Find(obj => obj.Id == installment_id);
                //        var diasVencidos = (int)DateTime.Now.Subtract(new DateTime(parcelaSelecionada.MaturityDate.Year, 
                //                                                                   parcelaSelecionada.MaturityDate.Month, 
                //                                                                   parcelaSelecionada.MaturityDate.Day)).TotalDays;

                //        var diaria = ((parcelaSelecionada.Value * accountToPay.Penalty) / 100);
                //        decimal? multaDiaria = (parcelaSelecionada.Value * (diasVencidos * accountToPay.DailyInterest) / 100);

                //        //pagamento parcelas
                //        var listaParcelas = new List<Installment>();
                //        var contador = 0;
                //        foreach (var item in accountToPay.Installments)
                //        {
                //            var modelInstallment = item;

                //            if (item.Id == valorParcela)
                //            {
                //                modelInstallment.PaymentDate = DateTime.Now;
                //                modelInstallment.PaidValue = (parcelaSelecionada.Value + diaria  +  multaDiaria).Value;
                //                modelInstallment.AccountToPayId = item.AccountToPayId;
                //            }

                //            if (item.PaidValue.HasValue) contador++;
                //            listaParcelas.Add(modelInstallment);
                //        }

                //        accountToPay.Installments = new List<Installment>();
                //        accountToPay.Installments = listaParcelas;

                //        if(listaParcelas.Count == contador)
                //        {
                //            // pagamento de conta sem estar vencida
                //            accountToPay.PaidValue = listaParcelas.Sum(obj => obj.PaidValue);
                //            accountToPay.PaymentDate = DateTime.Now;
                //            accountToPay.PaymentType = paymentType;
                //        }
                //    }
                //}
                //else
                //{

                //    if (isConta)
                //    {
                //        // pagamento de conta sem estar vencida
                //        accountToPay.PaidValue = accountToPay.Value;
                //        accountToPay.PaymentDate = DateTime.Now;
                //        accountToPay.PaymentType = (PaymentType == 0 ? Domain.Enums.PaymentType.Money : PaymentType ==  1 ? 
                //                                                Domain.Enums.PaymentType.BankTransfer : Domain.Enums.PaymentType.BankDeposit);

                //        //pagamento parcelas
                //        var listaParcelas = new List<Installment>();
                //        foreach (var item in accountToPay.Installments)
                //        {
                //            var modelInstallment = item;
                //            modelInstallment.PaymentDate = DateTime.Now;
                //            modelInstallment.PaidValue = item.Value;

                //            listaParcelas.Add(modelInstallment);
                //        }

                //        accountToPay.Installments = new List<Installment>();
                //        accountToPay.Installments = listaParcelas;
                //    }
                //    else
                //    {
                //        //pagamento parcelas
                //        var listaParcelas = new List<Installment>();
                //        foreach (var item in accountToPay.Installments)
                //        {
                //            var modelInstallment = item;

                //            if (item.Id == valorParcela)
                //            {
                //                modelInstallment.PaymentDate = DateTime.Now;
                //                modelInstallment.PaidValue = item.Value;
                //                modelInstallment.AccountToPayId = item.AccountToPayId;
                //            }

                //            listaParcelas.Add(modelInstallment);
                //        }

                //        accountToPay.Installments = new List<Installment>();
                //        accountToPay.Installments = listaParcelas;
                //    }
                //}

                //_accountToPayRepository.Update(accountToPay);

                //if (isConta && accountToPay.MonthlyAccount)
                //{
                //    var objModel = new AccountToPay
                //    {
                //        MaturityDate = accountToPay.MaturityDate.AddDays(30),
                //        Active = accountToPay.Active,
                //        AddedDate = accountToPay.AddedDate,
                //        ApplicationUser = accountToPay.ApplicationUser,
                //        ApplicationUserId = accountToPay.ApplicationUserId,
                //        DailyInterest = accountToPay.DailyInterest,
                //        Description = accountToPay.Description,
                //        DocumentNumber = accountToPay.DocumentNumber,
                //        Module = accountToPay.Module,
                //        ModuleId = accountToPay.ModuleId,
                //        MonthlyAccount = accountToPay.MonthlyAccount,
                //        NumberOfInstallments = accountToPay.NumberOfInstallments,
                //        PaymentSimulators = accountToPay.PaymentSimulators,
                //        Penalty = accountToPay.Penalty,
                //        Priority = accountToPay.Priority,
                //        Provider = accountToPay.Provider,
                //        ProviderId = accountToPay.ProviderId,
                //        Value = accountToPay.Value
                //    };

                //    // criação das parcelas                   
                //    var lista = new List<Installment>();
                //    for (var i = 1; i <= accountToPay.NumberOfInstallments; i++)
                //    {
                //        var obj = new Installment
                //        {
                //            MaturityDate = i.Equals(1) ? accountToPay.MaturityDate : accountToPay.MaturityDate.AddDays(i * 30 - 30),
                //            Value = accountToPay.Value,
                //            AccountToPayId = 1,
                //            Active = true
                //        };
                //        lista.Add(obj);
                //    }

                //    objModel.Installments = new List<Installment>();
                //    objModel.Installments = lista;

                //    _accountToPayRepositoryAdd.Add(objModel);
                //}

                accountToPay.PaymentDate = DateTime.Now;

                this.AddToastMessage("Conta Paga", $"A Conta {accountToPay.Description} foi paga com sucesso", ToastType.Success);

                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var accountToPay = _accountToPayRepository.FindActive(id.Value);
            if (accountToPay == null)
            {
                return HttpNotFound();
            }

            ViewBag.Providers = new SelectList(_providerRepository.AllActivesProviderPhysicalLegalPerson, nameof(Provider.Id),
                nameof(IProviderPhysicalLegalPerson.NameCorporateName));

            return View(Mapper.Map<AccountToPayViewModel>(accountToPay));
        }

        [HttpPost, Authorize(Roles = "Administrador"), ValidateAntiForgeryToken]
        public ActionResult Edit(AccountToPayViewModel accountToPayViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var accountToPay = _accountToPayRepository.FindActive(accountToPayViewModel.Id);

                    accountToPay.Value = accountToPayViewModel.Value;
                    accountToPay.PaidValue = accountToPayViewModel.PaidValue;
                    accountToPay.PaymentDate = accountToPayViewModel.PaymentDate;
                    accountToPay.Description = accountToPayViewModel.Description;
                    accountToPay.MaturityDate = accountToPayViewModel.MaturityDate;
                    accountToPay.DailyInterest = accountToPayViewModel.DailyInterest;
                    accountToPay.MonthlyAccount = accountToPayViewModel.MonthlyAccount;
                    accountToPay.Priority = accountToPayViewModel.Priority;

                    _accountToPayRepository.Update(accountToPay);

                    this.AddToastMessage("Conta Editada", $"A Conta {accountToPay.Description} foi editada com sucesso",
                        ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Edição", $"Erro ao editar a conta {accountToPayViewModel.Description}, " +
                        "favor tentar novamente", ToastType.Error);
                }
            }

            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var accountToPay = _accountToPayRepository.Find(id.Value);

            return accountToPay == null ? HttpNotFound() : (ActionResult)View(Mapper.Map<AccountToPayViewModel>(accountToPay));
        }

        [HttpPost, Authorize(Roles = "Administrador"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var accountToPay = _accountToPayRepository.Find(id.Value);

                try
                {
                    if (accountToPay == null)
                    {
                        return HttpNotFound();
                    }

                    _accountToPayRepository.DeleteLogical(accountToPay);

                    this.AddToastMessage("Conta excluída", $"A Conta {accountToPay.Description} foi excluída com sucesso",
                        ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Exclusão", $"Erro ao excluir a Conta {accountToPay?.Description}, " +
                        "favor tentar novamente", ToastType.Error);
                }
            }

            return View();
        }
    }
}

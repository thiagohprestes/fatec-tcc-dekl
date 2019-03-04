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
using DEKL.CP.UI.ViewModels.InternalBankAccount;
using DEKL.CP.UI.ViewModels.Provider;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Domain.Enums;

namespace DEKL.CP.UI.Controllers
{
    public class AccountToPayController : Controller
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAccountToPayRepository _accountToPayRepository;
        private readonly IInternalBankAccountRepository _internalBankAccountRepository;
        private readonly IProviderBankAccountRepository _providerBankAccountRepository;

        public int TypePayment { get; private set; }

        public AccountToPayController(IAccountToPayRepository accountToPayRepository, IProviderRepository providerRepository, IInternalBankAccountRepository internalBankAccountRepository, IProviderBankAccountRepository providerBankAccountRepository)
        {
            _providerRepository = providerRepository;
            _internalBankAccountRepository = internalBankAccountRepository;
            _providerBankAccountRepository = providerBankAccountRepository;
            _accountToPayRepository = accountToPayRepository;

        }

        public ActionResult Index() => View(Mapper.Map<IEnumerable<AccountToPayRelashionships>>(_accountToPayRepository.AccountToPayActivesRelashionships));

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
                {
                    if (accountToPay.Installments?.Count > 0)
                    {
                        var valueInstallment = accountToPay.Value / accountToPay.Installments.Count;

                        var installment = new Installment
                        {
                            Value = valueInstallment,
                            MaturityDate = new DateTime(accountToPay.MaturityDate.Year,
                                accountToPay.MaturityDate.Month,
                                DateTime.DaysInMonth(accountToPay.MaturityDate.Year, accountToPay.MaturityDate.Month))
                        };
                    }

                    accountToPay.ApplicationUserId = User.Identity.GetUserId<int>();
                    _accountToPayRepository.Add(accountToPay);

                    this.AddToastMessage("Conta salva", $"A conta {accountToPay.Description} foi salva com sucesso", ToastType.Success);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Providers = new SelectList(_providerRepository.AllActivesProviderPhysicalLegalPerson, nameof(Provider.Id),
                        nameof(IProviderPhysicalLegalPerson.NameCorporateName));

                    this.AddToastMessage("Erro no salvamento", $"Erro ao salvar a conta {accountToPay.Description}, " +
                        "favor tentar novamente", ToastType.Error);

                }
            }

            return View();
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

        public ActionResult InstallmentPayment(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var accountToPay = _accountToPayRepository.Find((int)id);

            ViewBag.InternalBankAccounts = new SelectList(Mapper.Map<IEnumerable<InternalBankAccountRelashionshipsViewModel>>(_internalBankAccountRepository.InternalBankAccountRelashionships), 
                                                          nameof(InternalBankAccountRelashionshipsViewModel.Id),
                                                          nameof(InternalBankAccountRelashionshipsViewModel.DescriptionAccount));      

            ViewBag.ProviderBankAccounts = new SelectList(Mapper.Map<IEnumerable<ProviderBankAccountRelashionshipsViewModel>>(_providerBankAccountRepository.ProviderBankAccountRelashionships(accountToPay.ProviderId)), 
                                                          nameof(ProviderBankAccountRelashionshipsViewModel.Id), 
                                                          nameof(ProviderBankAccountRelashionshipsViewModel.DescriptionAccount));

            return View(Mapper.Map<AccountToPayViewModel>(_accountToPayRepository.Find(id.Value)));
        }

        public ActionResult PaymentBoleto(int? PaymentInterna, int? PaymentBancario, int? PaymentType, int id, int valorParcela)
        {
            if (id == 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            bool isConta = valorParcela.Equals(0);

            var model = _accountToPayRepository.FindActive(id);

            if (!isConta && DateTime.Now > model.Installments.ToList().Find(obj => obj.Id == valorParcela).MaturityDate || DateTime.Now > model.MaturityDate)
            {
                if (isConta)
                {
                    var diasVencidos = (int)DateTime.Now.Subtract(new DateTime(model.MaturityDate.Year, model.MaturityDate.Month, model.MaturityDate.Day)).TotalDays;
                    var diaria = model.Value + ((model.Value * model.Penalty) / 100);
                    decimal? multaDiaria = (diaria * (diasVencidos * model.DailyInterest) / 100);

                    model.PaidValue = (diaria + multaDiaria).Value;

                    // pagamento de conta sem estar vencida
                    model.PaidValue = model.PaidValue;
                    model.PaymentDate = DateTime.Now;
                    model.PaymentType = (PaymentType == 0 ? DEKL.CP.Domain.Enums.PaymentType.Money : (PaymentType == 1 ?
                                            DEKL.CP.Domain.Enums.PaymentType.BankTransfer : DEKL.CP.Domain.Enums.PaymentType.BankDeposit));

                    //pagamento parcelas
                    var listaParcelas = new List<Installment>();
                    foreach (var item in model.Installments)
                    {
                        var modelInstallment = new Installment();

                        modelInstallment = item;
                        modelInstallment.PaymentDate = DateTime.Now;
                        modelInstallment.PaidValue = item.Value;

                        listaParcelas.Add(modelInstallment);
                    }

                    model.Installments = new List<Installment>();
                    model.Installments = listaParcelas;

                }
                else
                {
                    var parcelaSelecionada =  model.Installments.ToList().Find(obj => obj.Id == valorParcela);
                    var diasVencidos = (int)DateTime.Now.Subtract(new DateTime(parcelaSelecionada.MaturityDate.Year, parcelaSelecionada.MaturityDate.Month, parcelaSelecionada.MaturityDate.Day)).TotalDays;
                    var diaria = ((parcelaSelecionada.Value * model.Penalty) / 100);
                    decimal? multaDiaria = (parcelaSelecionada.Value * (diasVencidos * model.DailyInterest) / 100);

                    //pagamento parcelas
                    var listaParcelas = new List<Installment>();
                    int contador = 0;
                    foreach (var item in model.Installments)
                    {
                        var modelInstallment = new Installment();

                        modelInstallment = item;

                        if (item.Id == valorParcela)
                        {
                            modelInstallment.PaymentDate = DateTime.Now;
                            modelInstallment.PaidValue = (parcelaSelecionada.Value + diaria  +  multaDiaria).Value;
                            modelInstallment.AccountToPayId = 1;
                        }

                        if (item.PaidValue.HasValue) contador++;
                        listaParcelas.Add(modelInstallment);
                    }

                    model.Installments = new List<Installment>();
                    model.Installments = listaParcelas;

                    if(listaParcelas.Count == contador)
                    {
                        // pagamento de conta sem estar vencida
                        model.PaidValue = listaParcelas.Sum(obj => obj.PaidValue);
                        model.PaymentDate = DateTime.Now;
                        model.PaymentType = (PaymentType == 0 ? DEKL.CP.Domain.Enums.PaymentType.Money : (PaymentType == 1 ?
                                                DEKL.CP.Domain.Enums.PaymentType.BankTransfer : DEKL.CP.Domain.Enums.PaymentType.BankDeposit));
                    }
                }
            }
            else
            {
                
                if (isConta)
                {
                    // pagamento de conta sem estar vencida
                    model.PaidValue = model.Value;
                    model.PaymentDate = DateTime.Now;
                    model.PaymentType = (PaymentType == 0 ? DEKL.CP.Domain.Enums.PaymentType.Money : (PaymentType ==  1 ? 
                                            DEKL.CP.Domain.Enums.PaymentType.BankTransfer : DEKL.CP.Domain.Enums.PaymentType.BankDeposit));

                    //pagamento parcelas
                    var listaParcelas = new List<Installment>();
                    foreach (var item in model.Installments)
                    {
                        var modelInstallment = new Installment();

                        modelInstallment = item;
                        modelInstallment.PaymentDate = DateTime.Now;
                        modelInstallment.PaidValue = item.Value;

                        listaParcelas.Add(modelInstallment);
                    }

                    model.Installments = new List<Installment>();
                    model.Installments = listaParcelas;
                    
                    // debitar 
                    //var conta = Mapper.Map<InternalBankAccountRelashionshipsViewModel>(_accountToPayRepository.Find(PaymentInterna.Value));
  
                }
                else
                {
                    //pagamento parcelas
                    var listaParcelas = new List<Installment>();
                    foreach (var item in model.Installments)
                    {
                        var modelInstallment = new Installment();

                        modelInstallment = item;

                        if (item.Id == valorParcela)
                        {
                            modelInstallment.PaymentDate = DateTime.Now;
                            modelInstallment.PaidValue = item.Value;
                            modelInstallment.AccountToPayId = 1;
                        }

                        listaParcelas.Add(modelInstallment);
                    }

                    model.Installments = new List<Installment>();
                    model.Installments = listaParcelas;
                }
            }

            _accountToPayRepository.Update(model);

            this.AddToastMessage("Conta Paga", $"A Conta {model.Description} foi paga com sucesso", ToastType.Success);

            return Redirect("/AccountToPay");
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

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var accountToPay = _accountToPayRepository.Find(id.Value);

            return accountToPay == null ? HttpNotFound() : (ActionResult)View(Mapper.Map<AccountToPayViewModel>(accountToPay));
        }

        [HttpPost, ActionName("Delete")]
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

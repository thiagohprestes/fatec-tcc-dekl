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
                {
                    if (accountToPay.Installments?.Count > 0)
                    {
                        var valueInstallment = accountToPay.Value / accountToPay.Installments.Count;
                    }

                    accountToPay.ApplicationUserId = User.Identity.GetUserId<int>();

                    // criação das parcelas                   
                    var lista = new List<Installment>();
                    for (int i = 1; i <= accountToPay.NumberOfInstallments; i++)
                    {
                        var obj = new Installment();
                        obj.MaturityDate =  i.Equals(1) ? accountToPay.MaturityDate : accountToPay.MaturityDate.AddDays(i * 30 - 30);
                        obj.Value = accountToPay.Value;
                        obj.AccountToPayId = 1;
                        obj.Active = true;
                        lista.Add(obj);
                    }

                    if (accountToPay.Installments == null) accountToPay.Installments = lista;

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
            var isConta = valorParcela.Equals(0);
            var model = _accountToPayRepository.FindActive(id);

            if (!isConta && DateTime.Now > model.Installments.ToList().Find(obj => obj.Id == valorParcela).MaturityDate 
                || DateTime.Now > model.MaturityDate)
            {
                if (isConta)
                {
                    var diasVencidos = (int)DateTime.Now.Subtract(new DateTime(model.MaturityDate.Year, 
                                                                               model.MaturityDate.Month, 
                                                                               model.MaturityDate.Day)).TotalDays;

                    var diaria = model.Value + ((model.Value * model.Penalty) / 100);
                    decimal? multaDiaria = (diaria * (diasVencidos * model.DailyInterest) / 100);

                    model.PaidValue = (diaria + multaDiaria).Value;

                    // pagamento de conta sem estar vencida
                    model.PaidValue = model.PaidValue;
                    model.PaymentDate = DateTime.Now;
                    model.PaymentType = (PaymentType == 0 ? Domain.Enums.PaymentType.Money : (PaymentType == 1 ?
                                            Domain.Enums.PaymentType.BankTransfer : Domain.Enums.PaymentType.BankDeposit));

                    //pagamento parcelas
                    var listaParcelas = new List<Installment>();
                    foreach (var item in model.Installments)
                    {
                        var modelInstallment = item;
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
                    var diasVencidos = (int)DateTime.Now.Subtract(new DateTime(parcelaSelecionada.MaturityDate.Year, 
                                                                               parcelaSelecionada.MaturityDate.Month, 
                                                                               parcelaSelecionada.MaturityDate.Day)).TotalDays;

                    var diaria = ((parcelaSelecionada.Value * model.Penalty) / 100);
                    decimal? multaDiaria = (parcelaSelecionada.Value * (diasVencidos * model.DailyInterest) / 100);

                    //pagamento parcelas
                    var listaParcelas = new List<Installment>();
                    var contador = 0;
                    foreach (var item in model.Installments)
                    {
                        var modelInstallment = item;

                        if (item.Id == valorParcela)
                        {
                            modelInstallment.PaymentDate = DateTime.Now;
                            modelInstallment.PaidValue = (parcelaSelecionada.Value + diaria  +  multaDiaria).Value;
                            modelInstallment.AccountToPayId = item.AccountToPayId;
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
                        model.PaymentType = (PaymentType == 0 ? Domain.Enums.PaymentType.Money : (PaymentType == 1 ?
                                                Domain.Enums.PaymentType.BankTransfer : Domain.Enums.PaymentType.BankDeposit));
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
                    model.PaymentType = (PaymentType == 0 ? Domain.Enums.PaymentType.Money : PaymentType ==  1 ? 
                                                            Domain.Enums.PaymentType.BankTransfer : Domain.Enums.PaymentType.BankDeposit);

                    //pagamento parcelas
                    var listaParcelas = new List<Installment>();
                    foreach (var item in model.Installments)
                    {
                        var modelInstallment = item;
                        modelInstallment.PaymentDate = DateTime.Now;
                        modelInstallment.PaidValue = item.Value;

                        listaParcelas.Add(modelInstallment);
                    }

                    model.Installments = new List<Installment>();
                    model.Installments = listaParcelas;
                }
                else
                {
                    //pagamento parcelas
                    var listaParcelas = new List<Installment>();
                    foreach (var item in model.Installments)
                    {
                        var modelInstallment = item;

                        if (item.Id == valorParcela)
                        {
                            modelInstallment.PaymentDate = DateTime.Now;
                            modelInstallment.PaidValue = item.Value;
                            modelInstallment.AccountToPayId = item.AccountToPayId;
                        }

                        listaParcelas.Add(modelInstallment);
                    }

                    model.Installments = new List<Installment>();
                    model.Installments = listaParcelas;
                }
            }

            _accountToPayRepository.Update(model);

            if (isConta && model.MonthlyAccount)
            {
                var objModel = new AccountToPay();
                objModel.MaturityDate = model.MaturityDate.AddDays(30);
                objModel.Active = model.Active;
                objModel.AddedDate = model.AddedDate;
                objModel.ApplicationUser = model.ApplicationUser;
                objModel.ApplicationUserId = model.ApplicationUserId;
                objModel.DailyInterest = model.DailyInterest;
                objModel.Description = model.Description;
                objModel.DocumentNumber = model.DocumentNumber;
                objModel.Module = model.Module;
                objModel.ModuleId = model.ModuleId;
                objModel.MonthlyAccount = model.MonthlyAccount;
                objModel.NumberOfInstallments = model.NumberOfInstallments;
                objModel.PaymentSimulators = model.PaymentSimulators;
                objModel.Penalty = model.Penalty;
                objModel.Priority = model.Priority;
                objModel.Provider = model.Provider;
                objModel.ProviderId = model.ProviderId;
                objModel.Value = model.Value;

                // criação das parcelas                   
                var lista = new List<Installment>();
                for (int i = 1; i <= model.NumberOfInstallments; i++)
                {
                    var obj = new Installment();
                    obj.MaturityDate = i.Equals(1) ? model.MaturityDate : model.MaturityDate.AddDays(i * 30 - 30);
                    obj.Value = model.Value;
                    obj.AccountToPayId = 1;
                    obj.Active = true;
                    lista.Add(obj);
                }

                objModel.Installments = new List<Installment>();
                objModel.Installments = lista;

                _accountToPayRepositoryAdd.Add(objModel);
            }
            
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

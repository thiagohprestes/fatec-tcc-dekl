using AutoMapper;
using DEKL.CP.Domain.Contracts.Entities;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels.AccountsToPay;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class AccountToPayController : Controller
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAccountToPayRepository _accountToPayRepository;

        public AccountToPayController(IAccountToPayRepository accountToPayRepository, IProviderRepository providerRepository)
        {
            _providerRepository = providerRepository;
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

                        new Installment
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
                        $"favor tentar novamente", ToastType.Error);

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
                    //accountToPay.Name = accountToPayViewModel.Name;
                    //accountToPay.Number = accountToPayViewModel.Number;

                    _accountToPayRepository.Update(accountToPay);

                    this.AddToastMessage("Conta Editada", $"A Conta {accountToPay.Description} foi editada com sucesso", 
                        ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Edição", $"Erro ao editar a conta {accountToPayViewModel.Description}, " +
                        $"favor tentar novamente", ToastType.Error);
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
                        $"favor tentar novamente", ToastType.Error);
                }
            }

            return View();
        }
    }
}

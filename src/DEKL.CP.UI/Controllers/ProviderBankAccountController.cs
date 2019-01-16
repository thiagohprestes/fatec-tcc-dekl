using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels.Provider;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class ProviderBankAccountController : Controller
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderBankAccountRepository _providerBankAccountRepository;

        public ProviderBankAccountController(IProviderRepository providerRepository, IProviderBankAccountRepository bankRepositoy)
        {
            _providerRepository = providerRepository;
            _providerBankAccountRepository = bankRepositoy;
        }

        public ActionResult Index() 
            => View(Mapper.Map<IEnumerable<ProviderBankAccountRelashionshipsViewModel>>(_providerBankAccountRepository.ProviderBankAccountActivesRelashionships));

        public ActionResult Create()
        {
            ViewBag.BankAgencies = new SelectList(_providerBankAccountRepository.BankAgencyesActives, nameof(BankAgency.Id), 
                nameof(BankAgency.BankAgencyDescription));
            ViewBag.Providers = new SelectList(_providerRepository.AllActivesProviderPhysicalLegalPerson, nameof(Provider.Id),
                nameof(ProviderPhysicalLegalPersonViewModel.NameCorporateName));

            return View();
        }

        [HttpPost]
        public ActionResult Create(ProviderBankAccount providerBankAccount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    providerBankAccount.ApplicationUserId = User.Identity.GetUserId<int>();
                    _providerBankAccountRepository.Add(providerBankAccount);

                    this.AddToastMessage("Conta salva", $"A conta {providerBankAccount.Name} foi salva com sucesso", ToastType.Success);
                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro no salvamento", $"Erro ao salvar a conta {providerBankAccount.Name}, favor tentar novamente",
                        ToastType.Error);
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

            var providerBankAccount = _providerBankAccountRepository.ProviderBankAccountRelashionships(id.Value);

            return View(Mapper.Map<ProviderBankAccountRelashionshipsViewModel>(providerBankAccount));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var providerBankAccount = _providerBankAccountRepository.FindActive(id.Value);
            if (providerBankAccount == null)
            {
                return HttpNotFound();
            }

            ViewBag.BankAgencies = new SelectList(_providerBankAccountRepository.BankAgencyesActives, nameof(BankAgency.Id),
                nameof(BankAgency.BankAgencyDescription));
            ViewBag.Providers = new SelectList(_providerRepository.AllActivesProviderPhysicalLegalPerson, nameof(Provider.Id),
                nameof(ProviderPhysicalLegalPersonViewModel.NameCorporateName));
            return View(Mapper.Map<ProviderBankAccountViewModel>(providerBankAccount));
        }

        [HttpPost, Authorize(Roles = "Administrador"), ValidateAntiForgeryToken]
        public ActionResult Edit(ProviderBankAccountViewModel providerBankAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var providerBankAccount = _providerBankAccountRepository.FindActive(providerBankAccountViewModel.Id);

                    providerBankAccount.Number = providerBankAccountViewModel.Number;
                    providerBankAccount.Name = providerBankAccountViewModel.Name;
                    providerBankAccount.BankAgencyId = providerBankAccountViewModel.BankAgencyId;
                    providerBankAccount.ProviderId = providerBankAccountViewModel.ProviderId;
                    providerBankAccount.ApplicationUserId = User.Identity.GetUserId<int>();

                    _providerBankAccountRepository.Update(providerBankAccount);

                    this.AddToastMessage("Conta Editada", $"a conta {providerBankAccount.Name} foi editada com sucesso",
                        ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Edição", $"Erro ao editar a conta {providerBankAccountViewModel.Name}, favor tentar novamente",
                        ToastType.Error);
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

            var providerBankAccount = _providerBankAccountRepository.Find(id.Value);

            return providerBankAccount == null ? HttpNotFound() : (ActionResult)View(Mapper.Map<ProviderBankAccountViewModel>(providerBankAccount));
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

                var providerBankAccount = _providerBankAccountRepository.Find(id.Value);

                try
                {
                    if (providerBankAccount == null)
                    {
                        return HttpNotFound();
                    }

                    _providerBankAccountRepository.DeleteLogical(providerBankAccount);

                    this.AddToastMessage("Conta Excluída", $"A conta {providerBankAccount.Name} foi excluída com sucesso", ToastType.Success);
                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Exclusão", $"Erro ao excluir a conta {providerBankAccount?.Name}, favor tentar novamente",
                        ToastType.Error);
                }
            }

            return View();
        }
    }
}
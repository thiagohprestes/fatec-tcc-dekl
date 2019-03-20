using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels.InternalBankAccount;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    [Authorize]
    public class InternalBankAccountController : Controller
    {
        private readonly IInternalBankAccountRepository _internalBankAccountRepository;

        public InternalBankAccountController(IInternalBankAccountRepository internalBankAccountRepository) 
            => _internalBankAccountRepository = internalBankAccountRepository;

        public ActionResult Index() 
            => View(Mapper.Map<IEnumerable<InternalBankAccountRelashionshipsViewModel>>(_internalBankAccountRepository.InternalBankAccountRelashionships));

        public ActionResult Create()
        {
            ViewBag.BankAgencies = new SelectList(_internalBankAccountRepository.BankAgenciesActives, nameof(BankAgency.Id),
                nameof(BankAgency.BankAgencyDescription));

            return View();
        }

        [HttpPost]
        public ActionResult Create(InternalBankAccount internalBankAccount)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    internalBankAccount.ApplicationUserId = User.Identity.GetUserId<int>();
                    _internalBankAccountRepository.Add(internalBankAccount);

                    this.AddToastMessage("Conta salva", $"A conta {internalBankAccount.Name} foi salva com sucesso", ToastType.Success);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.BankAgencies = new SelectList(_internalBankAccountRepository.BankAgenciesActives, nameof(BankAgency.Id),
                        nameof(BankAgency.BankAgencyDescription));

                    this.AddToastMessage("Erro no salvamento", $"Erro ao salvar a conta {internalBankAccount.Name}, favor tentar novamente",
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

            var internalBankAccount = _internalBankAccountRepository.FindActive(id.Value);

            return View(Mapper.Map<InternalBankAccountViewModel>(internalBankAccount));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var providerBankAccount = _internalBankAccountRepository.FindActive(id.Value);
            if (providerBankAccount == null)
            {
                return HttpNotFound();
            }

            ViewBag.BankAgencies = new SelectList(_internalBankAccountRepository.BankAgenciesActives, nameof(BankAgency.Id),
                nameof(BankAgency.BankAgencyDescription));
            return View(Mapper.Map<InternalBankAccountViewModel>(providerBankAccount));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(InternalBankAccountViewModel internalBankAccountViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    internalBankAccountViewModel.ApplicationUserId = User.Identity.GetUserId<int>();

                    _internalBankAccountRepository.Update(Mapper.Map<InternalBankAccount>(internalBankAccountViewModel));

                    this.AddToastMessage("Conta Editada", $"a conta {internalBankAccountViewModel.Name} foi editada com sucesso",
                        ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Edição", $"Erro ao editar a conta {internalBankAccountViewModel.Name}, " +
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

            var internalBankAccount = _internalBankAccountRepository.Find(id.Value);

            return internalBankAccount == null ? 
                HttpNotFound() : 
                (ActionResult)View(Mapper.Map<InternalBankAccountViewModel>(internalBankAccount));
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

                var internalBankAccount = _internalBankAccountRepository.Find(id.Value);

                try
                {
                    if (internalBankAccount == null)
                    {
                        return HttpNotFound();
                    }

                    _internalBankAccountRepository.DeleteLogical(internalBankAccount);

                    this.AddToastMessage("Conta excluída", $"A conta {internalBankAccount.Name} foi excluída com sucesso", ToastType.Success);
                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Exclusão", $"Erro ao excluir a conta {internalBankAccount?.Name}, favor tentar novamente",
                        ToastType.Error);
                }
            }

            return View();
        }
    }
}
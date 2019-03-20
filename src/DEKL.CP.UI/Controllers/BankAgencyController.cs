using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels.BankAgency;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    [Authorize]
    public class BankAgencyController : Controller
    {
        private readonly IBankAgencyRepository _bankAgencyRepository;

        public BankAgencyController(IBankAgencyRepository bankAgencyRepositoy) => _bankAgencyRepository = bankAgencyRepositoy;

        public ActionResult Index() => View(Mapper.Map<IEnumerable<BankAgencyViewModel>>(_bankAgencyRepository.Actives));

        public ActionResult Create()
        {
            ViewBag.Banks = new SelectList(_bankAgencyRepository.BanksActives, nameof(Bank.Id), nameof(Bank.Name));
            return View();
        }

        [HttpPost]
        public ActionResult Create(BankAgency bankAgency)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bankAgencyRepository.Add(bankAgency);

                    this.AddToastMessage("Banco salvo", $"A Agência de número {bankAgency.Number} foi salva com sucesso", 
                        ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Banks = new SelectList(_bankAgencyRepository.BanksActives, nameof(Bank.Id), nameof(Bank.Name));
                    this.AddToastMessage("Erro no salvamento", $"Erro ao salvar a Agência de número {bankAgency.Number}, " +
                                                                "favor tentar novamente",  ToastType.Error);
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

            return View(Mapper.Map<BankAgencyViewModel>(_bankAgencyRepository.Find(id.Value)));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bankAgency = _bankAgencyRepository.FindActive(id.Value);
            if (bankAgency == null)
            {
                return HttpNotFound();
            }

            ViewBag.Banks = new SelectList(_bankAgencyRepository.BanksActives, nameof(Bank.Id), nameof(Bank.Name));
            return View(Mapper.Map<BankAgencyViewModel>(bankAgency));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(BankAgencyViewModel bankAgencyViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var bankAgency = Mapper.Map<BankAgency>(bankAgencyViewModel);

                    _bankAgencyRepository.Update(bankAgency);

                    this.AddToastMessage("Banco Editado", $"A Agência de número {bankAgencyViewModel.Number} foi editada com sucesso", 
                        ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Edição", $"Erro ao editar a Agência de número {bankAgencyViewModel.Number}, " +
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

            var bankAgency = _bankAgencyRepository.Find(id.Value);

            return bankAgency == null ? HttpNotFound() : (ActionResult)View(Mapper.Map<BankAgencyViewModel>(bankAgency));
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

                var bankAgency = _bankAgencyRepository.Find(id.Value);

                try
                {
                    if (bankAgency == null)
                    {
                        return HttpNotFound();
                    }

                    _bankAgencyRepository.DeleteLogical(bankAgency);

                    this.AddToastMessage("Banco excluído", $"A Agência de número {bankAgency.Number} foi excluída com sucesso",
                        ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Exclusão", $"Erro ao excluir o Banco {bankAgency?.Number}, favor tentar novamente",
                        ToastType.Error);
                }
            }

            return View();
        }
    }
}

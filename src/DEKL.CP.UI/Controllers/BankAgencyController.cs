using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels.Bank;
using DEKL.CP.UI.ViewModels.BankAgency;

namespace DEKL.CP.UI.Controllers
{
    public class BankAgencyController : Controller
    {
        private readonly IBankAgencyRepository _bankAgencyRepository;

        public BankAgencyController(IBankAgencyRepository bankAgencyRepositoy)
        {
            _bankAgencyRepository = bankAgencyRepositoy;
        }

        public ActionResult Index() => View(Mapper.Map<IEnumerable<BankAgencyViewModel>>(_bankAgencyRepository.Actives));

        public ActionResult Create()
        {
            
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

                    this.AddToastMessage("Banco salvo", $"A Agência de número {bankAgency.Number} foi salvo com sucesso", 
                        ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
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

            return View(Mapper.Map<BankViewModel>(_bankAgencyRepository.Find(id.Value)));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bank = _bankAgencyRepository.FindActive(id.Value);
            if (bank == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<BankAgencyViewModel>(bank));
        }

        [HttpPost, Authorize(Roles = "Administrador"), ValidateAntiForgeryToken]
        public ActionResult Edit(BankAgencyViewModel bankAgencyViewModel)
        {
            if (ModelState.IsValid)
            {
                var bankAgency = _bankAgencyRepository.FindActive(bankAgencyViewModel.Id);

                try
                {
                    bankAgency.Number = bankAgencyViewModel.Number;
                    bankAgency.ManagerName = bankAgencyViewModel.ManagerName;

                    _bankAgencyRepository.Update(bankAgency);

                    this.AddToastMessage("Banco Editado", $"A Agência de número {bankAgency.Number} foi editado com sucesso", 
                        ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Edição", $"Erro ao editar a Agência de número {bankAgency.Number}, " +
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

            var bank = _bankAgencyRepository.Find(id.Value);

            return bank == null ? HttpNotFound() : (ActionResult)View(bank);
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

                var bankAgency = _bankAgencyRepository.Find(id.Value);

                try
                {
                    if (bankAgency == null)
                    {
                        return HttpNotFound();
                    }

                    _bankAgencyRepository.DeleteLogical(bankAgency);

                    this.AddToastMessage("Banco excluído", $"A Agência de número {bankAgency.Number} foi excluído com sucesso",
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

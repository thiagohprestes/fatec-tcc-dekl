using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels.Bank;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    [Authorize]
    public class BankController : Controller
    {
        private readonly IBankRepository _bankRepository;

        public BankController(IBankRepository bankRepositoy) => _bankRepository = bankRepositoy;

        public ActionResult Index() => View(Mapper.Map<IEnumerable<BankViewModel>>(_bankRepository.Actives));

        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(Bank bank)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bankRepository.Add(bank);

                    this.AddToastMessage("Banco salvo", $"O banco {bank.Name} foi salvo com sucesso", ToastType.Success);
                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro no salvamento", $"Erro ao salvar o Banco {bank.Name}, favor tentar novamente",
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

            return View(Mapper.Map<BankViewModel>(_bankRepository.Find(id.Value)));
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var bank = _bankRepository.FindActive(id.Value);
            if (bank == null)
            {
                return HttpNotFound();
            }

            return View(Mapper.Map<BankViewModel>(bank));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(BankViewModel bankViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bankRepository.Update(Mapper.Map<Bank>(bankViewModel));

                    this.AddToastMessage("Banco Editado", $"O banco {bankViewModel.Name} foi editado com sucesso", ToastType.Success);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Edição", $"Erro ao editar o Banco {bankViewModel.Name}, favor tentar novamente", 
                        ToastType.Error);
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

            var bank = _bankRepository.Find(id.Value);

            return bank == null ? HttpNotFound() : (ActionResult)View(Mapper.Map<BankViewModel>(bank));
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

                var bank = _bankRepository.Find(id.Value);

                try
                {
                    if (bank == null)
                    {
                        return HttpNotFound();
                    }

                    _bankRepository.DeleteLogical(bank);

                    this.AddToastMessage("Banco excluído", $"O banco {bank.Name} foi excluído com sucesso", ToastType.Success);
                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro na Exclusão", $"Erro ao excluir o Banco {bank?.Name}, favor tentar novamente", 
                        ToastType.Error);
                }
            }

            return View();
        }
    }
}

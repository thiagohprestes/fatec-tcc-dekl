using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Domain.Enums;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels.Provider;

namespace DEKL.CP.UI.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IStateRepository _stateRepository;
        private readonly IProviderRepository _providerRepository;

        public ProviderController(IStateRepository stateRepository, IProviderRepository providerRepository)
        {
            _stateRepository = stateRepository;
            _providerRepository = providerRepository;
        }

        public ActionResult Index() 
            => View(Mapper.Map<IEnumerable<ProviderPhysicalLegalPersonViewModel>>(_providerRepository.AllActivesProviderPhysicalLegalPerson));

        public ActionResult Select() => View();
 
        public ActionResult Create(TypeProvider typeprovider)
        {
            ViewBag.States = new SelectList(_stateRepository.Actives, nameof(State.Id), nameof(State.Name));
            return View(typeprovider == TypeProvider.PhysicalPerson ? "CreateProviderPhysicalPerson" : "CreateProviderLegalPerson");
        }

        public ActionResult CreateProviderPhysicalPerson(ProviderPhysicalPerson model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _providerRepository.Add(model);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro no salvamento", "Erro ao salvar o fornecedor, favor tentar novamente", ToastType.Error);
                }
            }

            ViewBag.States = new SelectList(_stateRepository.Actives, nameof(State.Id), nameof(State.Name));
            return View();
        }

        public ActionResult CreateProviderLegalPerson(ProviderLegalPerson model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _providerRepository.Add(model);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro no salvamento", "Erro ao salvar o fornecedor, favor tentar novamente", ToastType.Error);
                }
            }

            ViewBag.States = new SelectList(_stateRepository.Actives, nameof(State.Id), nameof(State.Name));
            return View();
        }

        public ActionResult Details(int? id, TypeProvider typeProvider)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return typeProvider == TypeProvider.PhysicalPerson ? 
                View("DetailsProviderPhysicalPerson", Mapper.Map<ProviderPhysicalPersonViewModel>(_providerRepository.FindActiveProviderPhysicalPerson(id.Value))) : 
                View("DetailsProviderLegalPerson", Mapper.Map<ProviderLegalPersonViewModel>(_providerRepository.FindActiveProviderLegalPerson(id.Value)));
        }
    }
}

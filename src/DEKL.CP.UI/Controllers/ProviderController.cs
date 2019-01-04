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
        private readonly IProviderRepository _providerRepository;

        public ProviderController(IProviderRepository providerRepository) => _providerRepository = providerRepository;

        public ActionResult Index() 
            => View(Mapper.Map<IEnumerable<ProviderPhysicalLegalPersonViewModel>>(_providerRepository.AllActivesProviderPhysicalLegalPerson));

        public ActionResult Select() => View();

        public ActionResult Create(TypeProvider typeprovider) 
            => View(typeprovider == TypeProvider.PhysicalPerson ? "CreateProviderPhysicalPerson" : "CreateProviderLegalPerson");

        public ActionResult CreateProviderPhysicalPerson(ProviderPhysicalPerson providerPhysicalPerson)
        {
            if (ModelState.IsValid)
            {
                try
                { 
                    _providerRepository.Add(providerPhysicalPerson);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro no salvamento", "Erro ao salvar o fornecedor, favor tentar novamente", ToastType.Error);
                    return View(providerPhysicalPerson);
                }
            }

            return View();
        }

        public ActionResult CreateProviderLegalPerson(ProviderLegalPerson providerLegalPerson)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _providerRepository.Add(providerLegalPerson);

                    return RedirectToAction("Index");
                }
                catch
                {
                    this.AddToastMessage("Erro no salvamento", "Erro ao salvar o fornecedor, favor tentar novamente", ToastType.Error);
                    return View(providerLegalPerson);
                }
            }

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

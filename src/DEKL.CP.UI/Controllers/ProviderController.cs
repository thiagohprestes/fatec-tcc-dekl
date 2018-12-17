using System;
using System.Web.Mvc;
using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Domain.Enums;
using DEKL.CP.Infra.Data.EF.Repositories;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels.Provider;

namespace DEKL.CP.UI.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IRepository<State> _stateRepository;
        private readonly IRepository<Provider> _providerRepository;
        private readonly IRepository<ProviderPhysicalPerson> _providerPhysicalPersonRepository;
        private readonly IRepository<ProviderLegalPerson> _providerLegalPersonRepository;

        public ProviderController(UnitOfWork unitOfWork)
        {
            _stateRepository = unitOfWork.RepositoryEF<State>();
            _providerRepository = unitOfWork.RepositoryEF<Provider>();
            _providerPhysicalPersonRepository = unitOfWork.RepositoryEF<ProviderPhysicalPerson>();
            _providerLegalPersonRepository = unitOfWork.RepositoryEF<ProviderLegalPerson>();
        }

        public ActionResult Index() => View();

        public ActionResult Select() =>View();
 
        public ActionResult Create(TypeProvider typeprovider)
        {
            ViewBag.States = new SelectList(_stateRepository.GetActives(), nameof(State.Initials), nameof(State.Name));
            return View(typeprovider == TypeProvider.PhysicalPerson ? "CreateProviderPhysicalPerson" : "CreateProviderLegalPerson");
        }

        public ActionResult CreateProviderPhysicalPerson(ProviderPhysicalPersonViewModel model)
        {
            try
            {
                var provider = new Provider
                {
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,

                };

                var providerPhysicalPerson = Mapper.Map<ProviderPhysicalPerson>(model);

                _providerRepository.Add(provider);
                _providerPhysicalPersonRepository.Add(providerPhysicalPerson);
            }
            catch (Exception ex)
            {
                this.AddToastMessage("Erro no salvamento", "Erro ao salvar o fornecedor, favor tentar novamente", ToastType.Error);
            }

            return View();
        }
    }
}

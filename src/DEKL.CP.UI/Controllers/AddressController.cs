using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class AddressController : Controller
    {
        private readonly IStateRepository _stateRepository;

        public AddressController(IStateRepository stateRepository) => _stateRepository = stateRepository;

        [ChildActionOnly]
        public ActionResult AddressPartialView()
        {
            ViewBag.States = new SelectList(_stateRepository.Actives, nameof(Bank.Id), nameof(Bank.Name));
            return PartialView("~/Views/Shared/_Address.cshtml");
        }
    }
}

using System.Linq;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using System.Web.Mvc;
using DEKL.CP.UI.ViewModels.Address;

namespace DEKL.CP.UI.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private readonly IStateRepository _stateRepository;

        public AddressController(IStateRepository stateRepository) => _stateRepository = stateRepository;

        [ChildActionOnly]
        public ActionResult AddressPartialView()
        {
            ViewBag.States = new SelectList(_stateRepository.Actives.Select(s => new State
            {
                Id = s.Id,
                Name = $"{s.Name} - {s.Initials}"
            }), nameof(State.Id), nameof(State.Name));
            return PartialView("~/Views/Shared/_Address.cshtml");
        }

        [ChildActionOnly]
        public ActionResult AddressPartialViewFilled(AddressViewModel addressViewModel)
        {
            ViewBag.States = new SelectList(_stateRepository.Actives.Select(s => new State
            {
                Id = s.Id,
                Name = $"{s.Name} - {s.Initials}"
            }), nameof(State.Id), nameof(State.Name));
            return PartialView("~/Views/Shared/_Address.cshtml", new AddressVM { Address = addressViewModel });
        }
    }
}

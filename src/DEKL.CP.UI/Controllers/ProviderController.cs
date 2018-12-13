using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class ProviderController : Controller
    {
        public ActionResult Index() => View();

        public ActionResult Select() => View();

        public ActionResult Create(int typeprovider)
        {
            ViewBag.PhysicalPerson = typeprovider;
            return View();
        }
    }
}

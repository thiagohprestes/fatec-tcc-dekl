using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class ProviderController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Select() => View();

        public ActionResult Create()
        {
            ViewBag.PhysicalPerson = true;
            return View();
        }
    }
}

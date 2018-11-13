using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult SignOut()
        {
            return View();
        }
    }
}

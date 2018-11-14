using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index() => View();

        public ActionResult About() => View();

        public ActionResult Contact() => View();

        public ActionResult SignOut() => View();
    }
}

using DEKL.CP.UI.ViewModels.Conta.Login;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class ContaController : Controller
    {
        [HttpGet]
        public ActionResult Login(string returnURL)
        {
            var model = new LoginVM() { ReturnURL = returnURL };
            return View(model);
        }
    }
}

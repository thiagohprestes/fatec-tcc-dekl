using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class BaseController : Controller
    {
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Error", error);
            }
        }

    }
}

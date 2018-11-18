using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class UsersAdminController : BaseController
    {
        private readonly ApplicationUserManager _userManager;

        public UsersAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
        }

        public ActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.Nome, LastName = model.Sobrenome};
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new {userId = user.Id, code}, Request.Url?.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Confirme sua Conta", 
                    $"Por favor confirme sua conta clicando neste link: {callbackUrl}");
                return View("DisplayEmail");
            }

            AddErrors(result);

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}

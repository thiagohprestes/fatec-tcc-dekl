using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class UsersAdminController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public UsersAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, protocol: Request.Url.Scheme);
                    await _userManager.SendEmailAsync(user.Id, "Confirme sua Conta", "Por favor confirme sua conta clicando neste link: <a href='" + callbackUrl + "'></a>");
                    ViewBag.Link = callbackUrl;
                    return View("DisplayEmail");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}

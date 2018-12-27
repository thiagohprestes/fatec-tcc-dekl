using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using DEKL.CP.UI.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        private ApplicationUserManager _userManager;

        public ManageController(ApplicationUserManager userManager) => _userManager = userManager;

        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage = message != null ? message.GetDescription() : string.Empty;

            var userId = User.Identity.GetUserId<int>();

            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await _userManager.GetPhoneNumberAsync(userId),
                TwoFactor = await _userManager.GetTwoFactorEnabledAsync(userId),
                Logins = await _userManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(User.Identity.GetUserId())
            };

            return View(model);
        }

        public ActionResult RemoveLogin()
        {
            var linkedAccounts = _userManager.GetLogins(User.Identity.GetUserId<int>());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return View(linkedAccounts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            string message;
            var result = await _userManager.RemoveLoginAsync(User.Identity.GetUserId<int>(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }
                message = ManageMessageId.RemoveLoginSuccess.GetDescription();
            }
            else
            {
                message = ManageMessageId.Error.GetDescription();
            }

            return RedirectToAction("ManageLogins", new { Message = message });
        }

        public ActionResult AddPhoneNumber() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Gerar um token e enviar
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId<int>(), model.Number);
            if (_userManager.SmsService == null)
            {
                return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
            }

            var message = new IdentityMessage
            {
                Destination = model.Number,
                Body = $"O código de segurança é: {code}"
            };
            await _userManager.SmsService.SendAsync(message);

            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }


        [HttpPost]
        public ActionResult RememberBrowser()
        {
            var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(User.Identity.GetUserId());
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, rememberBrowserIdentity);
            return RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        public ActionResult ForgetBrowser()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            return RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await _userManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId<int>(), true);
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }

            return RedirectToAction("Index", "Manage");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await _userManager.SetTwoFactorEnabledAsync(User.Identity.GetUserId<int>(), false);
            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }

            return RedirectToAction("Index", "Manage");
        }

        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            await _userManager.GenerateChangePhoneNumberTokenAsync(User.Identity.GetUserId<int>(), phoneNumber);

            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userManager.ChangePhoneNumberAsync(User.Identity.GetUserId<int>(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }

                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess.GetDescription() });
            }

            // No caso de falha, reexibir a view. 
            ModelState.AddModelError("Error", @"Falha ao adicionar telefone");
            return View(model);
        }

        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await _userManager.SetPhoneNumberAsync(User.Identity.GetUserId<int>(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error.GetDescription() });
            }

            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
            if (user != null)
            {
                await SignInAsync(user, isPersistent: false);
            }

            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess.GetDescription() });
        }

        public ActionResult ChangePassword() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userManager.ChangePasswordAsync(User.Identity.GetUserId<int>(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                if (user != null)
                {
                    await SignInAsync(user, isPersistent: false);
                }

                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess.GetDescription() });
            }

            AddErrors(result);
            return View(model);
        }

        public ActionResult SetPassword() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.AddPasswordAsync(User.Identity.GetUserId<int>(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());
                    if (user != null)
                    {
                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess.GetDescription() });
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage = message.GetDescription();

            var user = await _userManager.FindByIdAsync(User.Identity.GetUserId<int>());

            if (user == null)
            {
                return View("Error");
            }

            var userLogins = await _userManager.GetLoginsAsync(User.Identity.GetUserId<int>());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes()
                .Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider))
                .ToList();

            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;

            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider) 
            => new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.Identity.GetUserId());

        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            var messageError = ManageMessageId.Error.GetDescription();

            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = messageError });
            }

            var result = await _userManager.AddLoginAsync(User.Identity.GetUserId<int>(), loginInfo.Login);
            return result.Succeeded
                ? RedirectToAction("ManageLogins")
                : RedirectToAction("ManageLogins", new { Message = messageError });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            var clientKey = Request.Browser.Type;
            await _userManager.SignInClientAsync(user, clientKey);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
                await user.GenerateUserIdentityAsync(_userManager));
        }

        private bool HasPassword() => _userManager.FindById(User.Identity.GetUserId<int>())?.PasswordHash != null;
        private bool HasPhoneNumber() => _userManager.FindById(User.Identity.GetUserId<int>())?.PasswordHash != null;

        public enum ManageMessageId
        {
            [Description("O Telefone foi adicionado.")]
            AddPhoneSuccess,

            [Description("A senha foi alterada.")]
            ChangePasswordSuccess,

            [Description("A segunda validação foi enviada.")]
            SetTwoFactorSuccess,

            [Description("A senha foi enviada.")]
            SetPasswordSuccess,

            [Description("O login externo foi removido")]
            RemoveLoginSuccess,

            [Description("O Telefone foi removido.")]
            RemovePhoneSuccess,

            [Description("Ocorreu um erro.")]
            Error
        }

        #endregion
    }
}
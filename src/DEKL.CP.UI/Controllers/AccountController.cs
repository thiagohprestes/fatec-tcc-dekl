using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using DEKL.CP.UI.Scripts.Toastr;
using Microsoft.AspNet.Identity;

namespace DEKL.CP.UI.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = await _userManager.FindAsync(model.Email, model.Password);
                    if (!user.EmailConfirmed)
                    {
                        this.AddToastMessage("Usuário não confirmado", "verifique seu e-mail", ToastType.Warning);
                    }

                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new {ReturnUrl = returnUrl});
                case SignInStatus.Failure:
                default:
                     ModelState.AddModelError("Error", @"Login ou Senha incorretos.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, int userId)
        {
            // Requer que o usuario já tenha feito um login por senha.
            if (!await _signInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(await _signInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                await _userManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }

            return View(new VerifyCodeViewModel {Provider = provider, ReturnUrl = returnUrl, UserId = userId});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _signInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false, 
                rememberBrowser: model.RememberBrowser);

            switch (result)
            {
                case SignInStatus.Success:
                    var user = _userManager.FindByIdAsync(model.UserId);
                    await SignInAsync(user.Result, false);
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("Error", @"Código Inválido.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(int? userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(userId.Value, code);

            if (!result.Succeeded)
            {
                ViewBag.MessageError = "Seu token expirou, favor gerar um novo";
                return View("Error");
            }

            return View("ConfirmEmail");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword() => View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
            {
                // Não revelar se o usuario nao existe ou nao esta confirmado
                return View("ForgotPasswordConfirmation");
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, Request.Url?.Scheme);
            await _userManager.SendEmailAsync(user.Id, "Esqueci minha senha", $"Por favor altere sua senha clicando aqui: {callbackUrl}");

            return View("ForgotPasswordConfirmation");

            // No caso de falha, reexibir a view. 
        }

        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation() => View();

        [AllowAnonymous]
        public ActionResult ResetPassword(string code) => code == null ? View("Error") : View();

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Não revelar se o usuario nao existe ou nao esta confirmado
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            var result = await _userManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }

            AddErrors(result);
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation() => View();

        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var userId = await _signInManager.GetVerifiedUserIdAsync();

            var userFactors = await _userManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem {Text = purpose, Value = purpose}).ToList();

            return View(new SendCodeViewModel {Providers = factorOptions, ReturnUrl = returnUrl, UserId = userId});
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Gerar o token e enviar
            if (!await _signInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }

            return RedirectToAction("VerifyCode",  new {Provider = model.SelectedProvider, model.ReturnUrl, userId = model.UserId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogOff()
        {
            await SignOutAsync();
            //AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            var clientKey = Request.Browser.Type;
            await _userManager.SignInClientAsync(user, clientKey);
            // Zerando contador de logins errados.
            await _userManager.ResetAccessFailedCountAsync(user.Id);

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie,
                DefaultAuthenticationTypes.TwoFactorCookie, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn
            (
                new AuthenticationProperties { IsPersistent = isPersistent },
                // Criação da instancia do Identity e atribuição dos Claims
                await user.GenerateUserIdentityAsync(_userManager)
            );
        }

        private async Task SignOutAsync()
        {
            var clientKey = Request.Browser.Type;
            var user = _userManager.FindById(User.Identity.GetUserId<int>());
            await _userManager.SignOutClientAsync(user, clientKey);
            AuthenticationManager.SignOut();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignOutEverywhere()
        {
            _userManager.UpdateSecurityStamp(User.Identity.GetUserId<int>());
            await SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOutClient(int clientId)
        {
            var user = _userManager.FindById(User.Identity.GetUserId<int>());
            var client = user.Clients.SingleOrDefault(c => c.Id == clientId);
            if (client != null)
            {
                user.Clients.Remove(client);
            }

            _userManager.Update(user);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            { }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}

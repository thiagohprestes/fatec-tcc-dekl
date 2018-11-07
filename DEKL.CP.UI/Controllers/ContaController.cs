using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Helpers;
using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace DEKL.CP.UI.Controllers
{
    public class ContaController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;

        public ContaController(ApplicationSignInManager signInManager, ApplicationUserManager userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public ActionResult Login(string returnURL)
        {
            var model = new LoginVM() { ReturnURL = returnURL };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginVM model, string returnURL)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var usuario = await _userManager.FindByEmailAsync(model.Email);

            if (usuario == null)
            {
                ModelState.AddModelError("", "Senha ou Usuário Inválidos");
                return View("Login");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(usuario.UserName, model.Password, model.RememberMe, shouldLockout: true);

            switch (signInResult)
            {
                case SignInStatus.Success:
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    return RedirectToLocal(returnURL);
                }
                case SignInStatus.LockedOut:
                {
                    var senhaCorreta = await _userManager.CheckPasswordAsync(usuario, model.Password);
                    if (senhaCorreta)
                        ModelState.AddModelError("", "A conta está bloqueada");
                    else
                        ModelState.AddModelError("", "Senha ou Usuário Inválidos");

                    return View(model);
                }
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnURL, model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Login ou Senha incorretos.");
                    return View(model);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult TrocarSenha()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult TrocarSenha(TrocaSenhaVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var usuario = _usuarioRepository.Get(_email);
        //            var senhaCriptografada = model.Senha.Encrypt();

        //            usuario.Senha = senhaCriptografada;
        //            _usuarioRepository.Edit(usuario);

        //            this.AddToastMessage("Troca de Senha", "Senha alterada com sucesso :-)", ToastType.Success);

        //        }
        //        catch (Exception ex)
        //        {
        //            this.AddToastMessage("Troca de Senha", ex.Message, ToastType.Success);
        //        }
        //    }

        //    return RedirectToAction("Index", "Home");
        //}

        //public ActionResult MeuPerfil()
        //{
        //    var usuario = _usuarioRepository.Get(_email);      

        //    return View(Mapper.Map<MeuPerfil>(usuario));
        //}

        //[HttpPost]
        //public ActionResult MeuPerfil(MeuPerfil model)
        //{
        //    var usuario = _usuarioRepository.Get(_email);

        //    try
        //    {
        //        usuario.Nome = model.Nome;
        //        usuario.Sobrenome = model.Sobrenome;
        //        _usuarioRepository.Edit(usuario);

        //        this.AddToastMessage("Alteração de Usuário", "Usuário alterado com sucesso :-)", ToastType.Success);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.AddToastMessage("Alteração de Usuário", ex.Message, ToastType.Success);
        //    }

        //    return RedirectToAction("Index", "Home");

        //}

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}

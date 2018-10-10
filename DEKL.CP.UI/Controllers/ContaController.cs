using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Helpers;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace DEKL.CP.UI.Controllers
{
    public class ContaController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private string _email { get => HttpContext.User.Identity.Name; }

        public ContaController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public ActionResult Login(string returnURL)
        {
            var model = new LoginVM() { ReturnURL = returnURL };
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            var usuario = _usuarioRepository.Get(model.Email);

            if (usuario == null)
            {
                ModelState.AddModelError("Email", "O e-mail não localizado");
            }
            else
            {
                if (usuario.Senha != model.Senha.Encrypt())
                {
                    ModelState.AddModelError("Senha", "Senha inválida");
                }
            }

            if (ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(model.Email, model.PermanecerLogado);

                if (!string.IsNullOrEmpty(model.ReturnURL) && Url.IsLocalUrl(model.ReturnURL))
                {
                    return Redirect(model.ReturnURL);
                }

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public ActionResult TrocarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TrocarSenha(TrocaSenhaVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = _usuarioRepository.Get(_email);
                    var senhaCriptografada = model.Senha.Encrypt();

                    usuario.Senha = senhaCriptografada;
                    _usuarioRepository.Edit(usuario);

                    this.AddToastMessage("Troca de Senha", "Senha alterada com sucesso :-)", ToastType.Success);

                }
                catch (Exception ex)
                {
                    this.AddToastMessage("Troca de Senha", ex.Message, ToastType.Success);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult MeuPerfil()
        {
            var usuario = _usuarioRepository.Get(_email);      

            return View(Mapper.Map<MeuPerfil>(usuario));
        }

        [HttpPost]
        public ActionResult MeuPerfil(MeuPerfil model)
        {
            var usuario = _usuarioRepository.Get(_email);

            try
            {
                usuario.Nome = model.Nome;
                usuario.Sobrenome = model.Sobrenome;
                _usuarioRepository.Edit(usuario);

                this.AddToastMessage("Alteração de Usuário", "Usuário alterado com sucesso :-)", ToastType.Success);
            }
            catch (Exception ex)
            {
                this.AddToastMessage("Alteração de Usuário", ex.Message, ToastType.Success);
            }

            return RedirectToAction("Index", "Home");

        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Login");
        }
    }
}

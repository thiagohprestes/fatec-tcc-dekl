﻿using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.Domain.Helpers;
using DEKL.CP.UI.ViewModels;
using System;
using System.Web.Mvc;
using System.Web.Security;

namespace DEKL.CP.UI.Controllers
{
    public class ContaController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

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
                ModelState.AddModelError("Email", "O e-mail não localizado");
            else
            {
                if (usuario.Senha != model.Senha.Encrypt())
                    ModelState.AddModelError("Senha", "Senha inválida");
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
                    var usuario = _usuarioRepository.Get(model.Email);
                    var senhaCriptografada = StringHelpers.Encrypt(model.Senha);

                    usuario.Senha = senhaCriptografada;
                    _usuarioRepository.Edit(usuario);

                    TempData["Mensagem"] = "Senha alterada com sucesso :-)";
                    TempData["Sucesso"] = true;
                }
                catch(Exception ex)
                {
                    TempData["Mensagem"] = ex.Message;
                    TempData["Sucesso"] = false;
                }
            }

            TempData["Titulo"] = "Troca de Senha";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}

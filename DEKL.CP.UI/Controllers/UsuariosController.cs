using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public ActionResult Index()
        {
            var usuarios = _usuarioRepository.Get();
            return View(Mapper.Map<IEnumerable<UsuarioVM>>(usuarios));
        }

        [HttpGet]
        public ViewResult AddEdit(int? id)
        {
            var usuario = new Usuario();

            if (id != null)
            {
                usuario = _usuarioRepository.Get(id.Value);
            }

            return View(Mapper.Map<UsuarioVM>(usuario));
        }

        [HttpPost]
        public ActionResult Add(UsuarioVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _usuarioRepository.Add(Mapper.Map<Usuario>(model));

                    TempData["Mensagem"] = "Usuário adicionado com sucesso :-)";
                    TempData["Sucesso"] = true;
                }
                catch (Exception)
                {
                    TempData["Mensagem"] = "Erro ao adicionar usuário :-(";
                    TempData["Sucesso"] = false;
                }              
            }

            TempData["Titulo"] = "Adição de Usuário";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(UsuarioVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuario = Mapper.Map<Usuario>(model);

                    _usuarioRepository.Edit(usuario);

                    TempData["Mensagem"] = "Usuário alterado com sucesso :-)";
                    TempData["Sucesso"] = true;
                }
                catch (Exception)
                {
                    TempData["Mensagem"] = "Erro ao editar usuário :-(";
                    TempData["Sucesso"] = false;
                }
            }

            TempData["Titulo"] = "Edição de Usuário";
            return RedirectToAction("Index");
        }
    }
}

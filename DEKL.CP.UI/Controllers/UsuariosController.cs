using AutoMapper;
using DEKL.CP.Domain.Contracts.Repositories;
using DEKL.CP.Domain.Entities;
using DEKL.CP.UI.Scripts.Toastr;
using DEKL.CP.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DEKL.CP.UI.Controllers
{
    [Authorize]
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
        public ActionResult AddEdit(UsuarioVM model)
        {
            if (model.Id == 0)
            {
                try
                {
                    _usuarioRepository.Add(Mapper.Map<Usuario>(model));
                    this.AddToastMessage("Adição de Usuário", "Usuário adicionado com sucesso :-)", ToastType.Success);
                }
                catch (Exception ex)
                {
                    this.AddToastMessage("Adição de Usuário", ex.Message, ToastType.Error);
                }
            }
            else
            {
                try
                {
                    var usuario = _usuarioRepository.Get(model.Id);
                    usuario.Nome = model.Nome;
                    usuario.Sobrenome = model.Sobrenome;
                    usuario.Email = model.Email;

                    _usuarioRepository.Edit(usuario);
                    this.AddToastMessage("Edição de Usuário", "Usuário editado com sucesso :-)", ToastType.Success);
                }
                catch (Exception ex)
                {
                    this.AddToastMessage("Adição de Usuário", ex.Message, ToastType.Error);
                }
            }

            return RedirectToAction("Index");
        }
    }
}

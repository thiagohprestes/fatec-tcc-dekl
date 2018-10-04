using DEKL.CP.Domain.Contracts.Repositories;
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
            return View(usuarios);
        }
    }
}

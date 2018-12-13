using System.Data.Entity;
using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.Infra.CrossCutting.Identity.Context;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using DEKL.CP.UI.Scripts.Toastr;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using Claim = System.Security.Claims.Claim;

namespace DEKL.CP.UI.Controllers
{
    public class ClaimsAdminController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationDbContext _dbContext;

        public ClaimsAdminController(ApplicationUserManager userManager, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public ActionResult Index() => View(_dbContext.Claims.ToList());

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var applicationClaim = await _dbContext.Claims.FirstAsync(c => c.Id == id);
             
            var userClaim = new ApplicationUserClaim { ClaimType = applicationClaim.Id.ToString() , ClaimValue = applicationClaim.Name };

            // GetAll the list of Users in this Claim
            var users = await _dbContext.Users.Where(u => u.Claims.Contains(userClaim)).ToListAsync();

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count();
            return View(applicationClaim);
        }

        public ActionResult SetUserClaim(int? id)
        {
            var userClaim = new ClaimViewModel
            {
                Types = new SelectList(_dbContext.Claims.ToList(), "Name", "Name")       
            };

            if (id != null)
            {
                ViewBag.User = _userManager.FindById(id.Value);
            }

            return View(userClaim);
        }

        [HttpPost]
        public ActionResult SetUserClaim(ClaimViewModel claim, int? id)
        {
            try
            {
                if (id != null) _userManager?.AddClaimAsync(id.Value, new Claim(claim.Type, claim.Value));
            }
            catch
            {
                this.AddToastMessage("Erro ao Associar Funcionalidade", 
                    "Ocorreu um erro ao associar funcionalidade ao usuário, favor tentar novamente", ToastType.Error);

                return RedirectToAction("Index", "UsersAdmin");
            }

            return RedirectToAction("Details", "UsersAdmin", new { id });
        }

        public ActionResult CreateClaim()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateClaim(Infra.CrossCutting.Identity.Models.Claim claim)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Claims.Add(claim);
                    _dbContext.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

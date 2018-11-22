using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.Infra.CrossCutting.Identity.Context;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using Microsoft.AspNet.Identity;

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

        public ActionResult Index()
        {
            return View(_dbContext.Claims.ToList());
        }

        public ActionResult SetUserClaim(int? id)
        {
            ViewBag.Type = new SelectList(_dbContext.Claims.ToList(), "Name", "Name");

            if (id != null) ViewBag.User = _userManager.FindById(id.Value);

            return View();
        }

        [HttpPost]
        public ActionResult SetUserClaim(ClaimViewModel claim, int? id)
        {
            try
            {
                _userManager.AddClaimAsync(id.Value, new Claim(claim.Type, claim.Value));

                return RedirectToAction("Details","UsersAdmin", new { id });
            }
            catch
            {
                return View();
            }
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

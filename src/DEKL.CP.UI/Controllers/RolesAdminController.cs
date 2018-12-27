using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using Microsoft.AspNet.Identity;

namespace DEKL.CP.UI.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RolesAdminController : BaseController
    {

        private readonly ApplicationRoleManager _roleManager;
        private readonly ApplicationUserManager _userManager;

        public RolesAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ActionResult Index() => View(_roleManager.Roles);

        public ActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                var role = new ApplicationRole(roleViewModel.Name);
                var roleresult = await _roleManager.CreateAsync(role);
                if (!roleresult.Succeeded)
                {
                    AddErrors(roleresult);
                    return View();
                }
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = await _roleManager.FindByIdAsync(id.Value);
            // GetAll the list of Users in this Role
            var users = new List<ApplicationUser>();

            // GetAll the list of Users in this Role
            foreach (var user in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(user.Id, role.Name))
                {
                    users.Add(user);
                }
            }

            ViewBag.Users = users;
            ViewBag.UserCount = users.Count;
            return View(role);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await _roleManager.FindByIdAsync(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            var roleModel = new RoleViewModel { Id = role.Id, Name = role.Name };
            return View(roleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Name,Id")] RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(roleModel.Id);
                role.Name = roleModel.Name;
                await _roleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = await _roleManager.FindByIdAsync(id.Value);
            return role == null ? HttpNotFound() : (ActionResult)View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id, string deleteUser)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var role = await _roleManager.FindByIdAsync(id.Value);
            if (role == null)
            {
                return HttpNotFound();
            }
            IdentityResult result;
            if (deleteUser != null)
            {
                result = await _roleManager.DeleteAsync(role);
            }
            else
            {
                result = await _roleManager.DeleteAsync(role);
            }

            if (result.Succeeded)
                return RedirectToAction("Index");

            AddErrors(result);
            return View();
        }
    }
}
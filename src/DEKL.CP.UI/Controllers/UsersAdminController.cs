using AutoMapper;
using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using DEKL.CP.UI.ViewModels.UsersAdmin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using DEKL.CP.UI.Scripts.Toastr;

namespace DEKL.CP.UI.Controllers
{
    public class UsersAdminController : BaseController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ActionResult> Index()
        {
            var users = await _userManager.Users.Where(u => u.Active).ToListAsync();

            return View(Mapper.Map<IEnumerable<ApplicationUsersViewModel>>(users));
        }

        public async Task<ActionResult> Create()
        {
            ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model, params string[] selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", string.Join("\\n", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)));
                ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
                return View();
            }

            //var user1 = Mapper.Map<ApplicationUser>(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = Regex.Replace(model.PhoneNumber, @"[^\d]", "")
            };

            var result = await _userManager.CreateAsync(user, model.PasswordHash);

            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, Request.Url?.Scheme);
                await _userManager.SendEmailAsync(user.Id, "Confirme sua Conta",
                    $"Por favor confirme sua conta clicando neste link: {callbackUrl}");

                if (selectedRoles != null)
                {
                    result = await _userManager.AddToRolesAsync(user.Id, selectedRoles);
                    if (!result.Succeeded)
                    {
                        ModelState.AddModelError("Error", result.Errors.First());
                        ViewBag.RoleId = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
                        return View();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Error", result.Errors.First());
                ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
                return View();
            }

            this.AddToastMessage("Usuário Adicionado", "Solicite que o usuário verifique o e-mail", ToastType.Success);
            return RedirectToAction("Index");
        }
    }
}

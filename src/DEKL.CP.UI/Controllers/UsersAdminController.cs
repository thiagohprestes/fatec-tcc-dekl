using AutoMapper;
using DEKL.CP.Infra.CrossCutting.Identity.Configuration;
using DEKL.CP.Infra.CrossCutting.Identity.Models;
using DEKL.CP.Infra.CrossCutting.Identity.ViewModels;
using DEKL.CP.UI.ViewModels.UsersAdmin;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using DEKL.CP.UI.Scripts.Toastr;

namespace DEKL.CP.UI.Controllers
{
    [Authorize(Roles = "Administrador")]
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

            return View(Mapper.Map<IEnumerable<ApplicationUsersViewModel>>(users) ?? new List<ApplicationUsersViewModel>());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await _userManager.FindByIdAsync(id.Value);

            ViewBag.RoleNames = await _userManager.GetRolesAsync(user.Id);

            return View(user);
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

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = Regex.Replace(model.PhoneNumber ?? string.Empty, @"[^\d]", string.Empty)
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

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await _userManager.FindByIdAsync(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user.Id);

            return View(new EditApplicationUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RolesList = _roleManager.Roles.ToList().Select(r => new SelectListItem
                {
                    Selected = userRoles.Contains(r.Name),
                    Text = r.Name,
                    Value = r.Name
                })
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FirstName,LastName,Email,Id")] EditApplicationUserViewModel model, 
            params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user == null)
                {
                    return HttpNotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.Email;
                user.Email = model.Email;

                var userRoles = await _userManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await _userManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("Error", result.Errors.First());
                    return View();
                }
                result = await _userManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("Error", result.Errors.First());
                    return View();
                }

                this.AddToastMessage("Edição de Usuário Salva", "O usuário foi editado com sucesso", ToastType.Success);
                return RedirectToAction("Index");
            }

            this.AddToastMessage("Erro na Edição do Usuário", "Ocorreu um erro na edição do usuário, favor tentar novamente", 
                                 ToastType.Success);
            return View();
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await _userManager.FindByIdAsync(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await _userManager.FindByIdAsync(id.Value);

                if (user == null)
                {
                    return HttpNotFound();
                }

                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("Error", result.Errors.First());
                    return View();
                }

                this.AddToastMessage("Usuário Excluído", "O usuário foi excluído com sucesso", ToastType.Success);
                return RedirectToAction("Index");
            }

            this.AddToastMessage("Erro na Exclusão do Usuário", "Ocorreu um erro na exclusão do usuário, favor tentar novamente", 
                                 ToastType.Success);
            return View();
        }

    }
}

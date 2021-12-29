using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YMovies.Identity.Managers;
using YMovies.Identity.Models;
using YMovies.Web.Dtos;
using YMovies.Web.Models.AdminViewModels;
using YMovies.Web.Utilities;

namespace YMovies.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        public AdminController()
        {
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
        }

        public ActionResult Find()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string email)
        {
            var user = await UserManager.FindByEmailAsync(email);

            if (user == null)
                return HttpNotFound();

            var roles = RoleManager.Roles.ToList();
            var rolesSelectedList = new SelectList(roles, "Name", "Name");

            var model = new RoleEditingModel
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = rolesSelectedList
            };

            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmEdit(string userId, string role = "user")
        {
            var temp = await RoleManager.FindByNameAsync(role);
            
            if (temp == null)
                return HttpNotFound();
            
            await UserManager.AddToRoleAsync(userId, temp.Name);

            return RedirectToAction("Index", "Home", null);
        }
    }
}
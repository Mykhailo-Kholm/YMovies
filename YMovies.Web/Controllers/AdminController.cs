using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YMovies.Identity.Dtos;
using YMovies.Identity.Managers;
using YMovies.Identity.Users;
using YMovies.Web.Utilities;

namespace YMovies.Web.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult ManageUsers()
        {
            IEnumerable<ApplicationUser> users = UserManager.Users.ToList();
            var models = AutoMap.Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserDto>>(users);

            return View(models);
        }
    }
}
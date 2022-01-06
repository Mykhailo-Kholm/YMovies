using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YMovies.Identity.Managers;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.Web.Models.AdminViewModels;

namespace YMovies.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        readonly IRepository<Movie> moviesRepo;

        public AdminController(IRepository<Movie> moviesRepo)
        {
            this.moviesRepo = moviesRepo;
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
            var model = new FindModel();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(FindModel Model)
        {
            if (!ModelState.IsValid)
                return View("Find", Model);

            var user = await UserManager.FindByEmailAsync(Model.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "This user isn't exists");
                return View("Find", Model);
            }

            var roles = RoleManager.Roles.ToList();
            var rolesSelectedList = new SelectList(roles, "Name", "Name");

            var model = new RoleEditingModel
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = rolesSelectedList
            };

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmEdit(string userId, string roles)
        {
            var user = await UserManager.FindByIdAsync(userId);
            var userRole = user.Roles.First();
            var role = await RoleManager.FindByIdAsync(userRole.RoleId);

            await UserManager.RemoveFromRoleAsync(userId, role.Name);
            await UserManager.AddToRoleAsync(userId, roles);

            return RedirectToAction("Index", "Home", null);
        }


        [HttpGet]
        public ActionResult CreateFilm() => View(new NewFilm());

        [HttpPost]
        public ActionResult CreateFilm(NewFilm model)
        {
            if(!ModelState.IsValid)
                return View(model);



        }
    }
}
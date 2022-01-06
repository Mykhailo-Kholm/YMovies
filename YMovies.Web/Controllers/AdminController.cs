using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YMovies.Identity.Managers;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.Web.Models.AdminViewModels;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers
{

    public class AdminController : Controller
    {
        readonly IRepository<Movie> _moviesRepo;
        readonly IRepository<Cast> _castsRepo;
        readonly IRepository<Genre> _genresRepo;

        public AdminController(IRepository<Movie> moviesRepo, IRepository<Cast> castsRepo, IRepository<Genre> genresRepo)
        {
            _moviesRepo = moviesRepo;
            _castsRepo = castsRepo;
            _genresRepo = genresRepo;
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
        public ActionResult CreateFilm()
        {
            return View("FilmCreation", new NewFilm());
        }

        private List<Cast> _casts = new List<Cast>()
        {
            new Cast()
            {
                Id = 1,
                Name = "First",
                Surname = "Actor"
            },
            new Cast()
            {
                Id = 2,
                Name = "Second",
                Surname = "Actor"
            },
            new Cast()
            {
                Id = 3,
                Name = "Third",
                Surname = "Actor"
            }
        };

        [HttpPost]
        public ActionResult Cast(string name)
        {
            var list = (from actor in _castsRepo.Items
                        where actor.Name.StartsWith(name)
                        select new
                        {
                            label = actor.Name + " " + actor.Surname,
                            id = actor.Id
                        }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateFilm(NewFilm model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return RedirectToAction("Index", "Home");
        }
    }

}
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ymovies.Identity.BLL.DTO;
using Ymovies.Identity.BLL.Interfaces;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
using YMovies.Web.Models.AdminViewModels;
using YMovies.Web.Utilites;
using YMovies.Web.Utilities;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers
{

    public class AdminController : Controller
    {
       

        public IIdentityUserService UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IIdentityUserService>();
            }
        }
        
        [HttpGet]
        public string Users(string query = null)
        {
            var userDto = UserManager.GetAllUsers();
            var viewModels = AutoMap.Mapper.Map<IEnumerable<UserDTO>, List<FindUserViewModel>>(userDto);
            if (!string.IsNullOrWhiteSpace(query))
                viewModels = viewModels.Where(u => u.Email.Contains(query)).ToList();
            return TypeConverter.ToJson(viewModels);
        }

        public ActionResult Find()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FindUserViewModel findModel)
        {
            if (!ModelState.IsValid)
                return View("Find", findModel);
            var user = UserManager.GetUserByEmail(findModel.Email);
            var model = AutoMap.Mapper.Map<UserDTO, ManageUserRightsViewModel>(user);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmEdit(string email, bool? adminRights)
        {
            if (adminRights.Value)
                await UserManager.ChangeUserAdminRightsByEmail(email);

            return RedirectToAction("Index", "Home", null);
        }

        [HttpGet]
        public ActionResult CreateFilm()
        {
            ViewBag.Types = _typesService.Items.ToList();
            return View("FilmCreation", new NewFilmViewModel());
        }


    }
}
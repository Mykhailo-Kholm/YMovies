using IMDbApiLib.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ymovies.Identity.BLL.Interfaces;
using YMovies.MovieDbService.Services.Service;
using YMovies.Web.IMDB;
using YMovies.Web.Models.AboutUs;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            _userService = new UserService(IdentityUserService);
            //ISeed dbseed = new DBSeed();
            //await dbseed.AddMovieByImbdId("tt0468569");
            return RedirectToAction("Index", "Movies");
        }
        private UserService _userService;

        public IIdentityUserService IdentityUserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IIdentityUserService>();
            }
        }

        public ActionResult About()
        {
            string FilePath = Server.MapPath("~/Json/");
            string fileName = "aboutus.json";
            var str = System.IO.File.ReadAllText(FilePath + fileName);

            AboutUsViewModel infoList = new JavaScriptSerializer().Deserialize<AboutUsViewModel>(str);

            return View(infoList);
        }
    }
}
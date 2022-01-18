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
            return RedirectToAction("Index", "Movies");
        }
        private UserService _userService;

        public IIdentityUserService IdentityUserService => HttpContext.GetOwinContext().GetUserManager<IIdentityUserService>();

        public ActionResult About()
        {
            string FilePath = Server.MapPath("~/Json/");
            string fileName = "aboutus.json";
            var str = System.IO.File.ReadAllText(FilePath + fileName);

            AboutUsViewModel infoList = new JavaScriptSerializer().Deserialize<AboutUsViewModel>(str);

            return View(infoList);
        }

        public async Task<ActionResult> Mock(string id)
        {
            APIworkerIMDB imdb = new APIworkerIMDB();

            ViewData["MovieReport"] = await imdb.ReportForMovieAsync(id);

            return View("MockFilm");
        }

        public async Task<ActionResult> Genre(string genre)
        {
            APIworkerIMDB imdb = new APIworkerIMDB();
            MovieModel movieModel = new MovieModel();
            var films = await imdb.GetOneHundredFilmsAsync(group: AdvancedSearchTitleGroup.Oscar_Winner);
            movieModel.Movies = new List<MovieGenreViewModel>();
            foreach (var film in films)
            {
                if (film.Genres.ToLower().Contains(genre.ToLower()))
                {
                    movieModel.Movies.Add(new MovieGenreViewModel()
                    {
                        id = film.Id,
                        Title = film.Title,
                        Genre = film.Genres,
                        Image = film.Image,
                        imDbRating = film.IMDbRating

                    }
                    );

                }
            }

            return View("MockSearch", movieModel);

        }
        public async Task<ActionResult> Title(string title)
        {
            APIworkerIMDB imdb = new APIworkerIMDB();
            MovieModel movieModel = new MovieModel();
            var films = await imdb.GetOneHundredFilmsAsync(group: AdvancedSearchTitleGroup.Oscar_Winner);
            movieModel.Movies = new List<MovieGenreViewModel>();
            foreach (var film in films)
            {
                if (film.Title.ToLower().Contains(title.ToLower()))
                {
                    movieModel.Movies.Add(new MovieGenreViewModel()
                    {
                        id = film.Id,
                        Title = film.Title,
                        Genre = film.Genres,
                        Image = film.Image,
                        imDbRating = film.IMDbRating
                    }
                    );

                }
            }

            return View("MockSearch", movieModel);

        }
    }
}
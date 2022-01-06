using IMDbApiLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.Web.IMDB;
using YMovies.Web.ViewModels;
using YMovies.MovieDbService.Repositories.Repository;

namespace YMovies.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Mock(string id)
        {
            APIworkerIMDB imdb = new APIworkerIMDB();

            ViewData["MovieReport"] = await imdb.ReportForMovie(id);

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
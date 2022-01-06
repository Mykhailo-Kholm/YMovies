using IMDbApiLib.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using YMovies.Web.IMDB;
using YMovies.Web.IMDB.DBWorker;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            ISeed dbseed = new DBSeed();

            dbseed.AddMovieByImbdId("tt0133093");
            return RedirectToAction("Index", "Movies");
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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
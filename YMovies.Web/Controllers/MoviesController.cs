using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using IMDbApiLib.Models;
using Microsoft.Ajax.Utilities;
using PagedList;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.Web.DTOs;
using YMovies.Web.IMDB;
using YMovies.Web.Services.Service;
using YMovies.Web.TempModels;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers
{
    public class MoviesController : Controller
    {
        public static MoviesContext context = new MoviesContext();
        static MovieRepository movieRepository = new MovieRepository(context);
        MovieWebService movieWebService = new MovieWebService(movieRepository);
        static CountryRepository countryRepository = new CountryRepository(context);
        CountryWebService countryWebService = new CountryWebService(countryRepository);
        private static GenreRepository genreRepository = new GenreRepository(context);
        private GenreWebService genreWebService = new GenreWebService(genreRepository);
        static TypeRepository typeRepository = new TypeRepository(context);
        TypeWebService typeWebService = new TypeWebService(typeRepository);

        public async Task<ActionResult> Like(int id)
        {
            return RedirectToAction("Details", id);

        }

        public async Task<ActionResult> DisLike(int id)
        {
            return RedirectToAction("Details", id);

        }

        public async Task<ActionResult> MostLiked()
        {
            return RedirectToAction("Index");

        }

        public async Task<ActionResult> MostWatched()
        {
            return RedirectToAction("Index");

        }

        public async Task<ActionResult> TopByIMDb(int? page)
        {
            APIworkerIMDB imdb = new APIworkerIMDB();
            var films = await imdb.GetTop250MoviesAsync();
            var pageSize = 50;
            int pageNumber = (page ?? 1);
            var topImdbViewModel = new TopImdbViewModel()
            {
                MoviePageList = films.ToPagedList(pageNumber, pageSize),
                Movies = films,
            };
            return View(topImdbViewModel);

        }

        public async Task<ActionResult> Search()
        {
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> Index(int? page, string action)
        {
       
            var pageSize = 10;
            var pageNumber = page ?? 1;
            List<MoviesInfo> moviesInfos = new List<MoviesInfo>();
            if (Request.UrlReferrer != null)
            {
                string prev = Request.UrlReferrer.ToString();
            }
            foreach (var movie in movieWebService.Items.ToList())
            {
                moviesInfos.Add
                (
                    new MoviesInfo(){Id = movie.MovieId, Title = movie.Title, PosterUrl = movie.PosterUrl,
                        ImdbRating = movie.ImdbRating, Genres = movie.Genres}
                );
            }
            var movieViewModel = new MovieViewModel()
            {
                MoviePageList = moviesInfos.ToPagedList(pageNumber, pageSize),
                Countries = countryWebService.Items,
                Genres = genreWebService.Items,
                Types = typeWebService.Items.DistinctBy(t=>t.Name),
                Years = movieWebService.Items.OrderBy(m=>m.Year).Select(m=>m.Year).Distinct().ToList(),
                MoviesInfo = moviesInfos
            };
            if (Session["Movies"] != null)
            {
                movieViewModel.MoviesInfo = Session["Movies"] as List<MoviesInfo>;
            }
            else
            {
                movieViewModel.MoviesInfo = moviesInfos;
            }
            //Session["Countries"] = countries;
            return View(movieViewModel);
        }

        public async Task<ActionResult> Details(int id)
        {
            MovieWebDto movie = movieWebService.GetItem(id);
            return View(movie);
        }

        public ActionResult Partial()
        {
            return PartialView(countryWebService.Items);
        }

        public async Task<ActionResult> FilterInclude(string action, int countryId)
        {

            List<MovieWebDto> newMovies = new List<MovieWebDto>();
            if (Session["Movies"] != null)
            {
                newMovies  = Session["Movies"] as List<MovieWebDto>;
            }

            //newMovies = movies.Movies.Where(p => countries.Any(p2 => countryId == p.Id)).ToList();
            //foreach (var m in movies)
            //{
            //    foreach (var c in m.Countries)
            //    {   
            //        if(c.Id==countryId)
            //            newMovies.Add(m);
            //    }
            //}

            Session["Movies"] = newMovies;
            //List<Movie> newMovies = movies.Where(p => countries.All(p2=>p2.Id==countryId)).ToList();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> FilterExclude(int countryId)
        {

            List<MovieWebDto> newMovies = new List<MovieWebDto>();
            if (Session["Movies"] != null)
            {
                newMovies = Session["Movies"] as List<MovieWebDto>;
            }

            //newMovies = movies.Movies.Where(p => countries.Any(p2 => countryId == p.Id)).ToList();
            //foreach (var m in movies)
            //{
            //    foreach (var c in m.Countries)
            //    {
            //        if (c.Id != countryId)
            //        {
            //            newMovies.Remove(m);
            //        }
            //        break;
            //    }
            //}

            Session["Movies"] = newMovies;
            //List<Movie> newMovies = movies.Where(p => countries.All(p2=>p2.Id==countryId)).ToList();
            return RedirectToAction("Index");
        }
    }
}
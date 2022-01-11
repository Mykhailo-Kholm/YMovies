using PagedList;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
using YMovies.Web.IMDB;
using YMovies.Web.IMDB.DBWorker;
using YMovies.Web.Models.MoviesInfoViewModel;
using YMovies.Web.Services.Service;
using YMovies.Web.TempModels;
using YMovies.Web.Utilites.Pagination;
using YMovies.Web.Utilities;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers
{
    public class MoviesController : Controller
    {
        public MoviesController(IService<MediaDto> movieService)
        {
            _movieService = movieService;
            _convertor = new ModelsConvertor();
        }

        private const int pageSize = 4;
        private readonly IService<MediaDto> _movieService;
        private ModelsConvertor _convertor;

        public async Task<ActionResult> Like(int id)
        {
            MediaDto movie = _movieService.GetItem(id);
            MediaDto newmovie = new MediaDto()
            {
                MediaId = movie.MediaId,

                NumberOfDislikes = ++movie.NumberOfLikes
            };
            _movieService.UpdateItem(newmovie);
            //likedmovies table
            return RedirectToAction("Details", id);
        }

        public async Task<ActionResult> DisLike(int id)
        {
            MediaDto movie = _movieService.GetItem(id);
            MediaDto newmovie = new MediaDto()
            {
                NumberOfDislikes = --movie.NumberOfLikes
            };
            _movieService.UpdateItem(newmovie);
            return RedirectToAction("Details", id);
        }

        //public async Task<ActionResult> MostLiked(int? page)
        //{
        //    var pageSize = 50;
        //    int pageNumber = (page ?? 1);
        //    var films = _movieService.Items.OrderByDescending(m => m.NumberOfLikes);
        //    var topImdbViewModel = new TopImdbViewModel()
        //    {
        //        //MoviePageList = convertor.ConvertToMoviesInfo(films).ToPagedList(pageNumber, pageSize),
        //        Movies = _convertor.ConvertToMoviesInfo(films),
        //    };
        //    return View("TopByIMDb", topImdbViewModel);
        //}

        public async Task<ActionResult> MostWatched(int? page)
        {
            //var pageSize = 50;
            //int pageNumber = (page ?? 1);
            return View("TopByIMDb");
        }

        public async Task<ActionResult> TopByIMDb(int? page)
        {
            var pageSize = 8;
            int pageNumber = (page ?? 1);
            List<MoviesInfo> moviesInfos = new List<MoviesInfo>();
            var movies = _movieService.Items.OrderByDescending(m => m.ImdbRating).Take(250).ToList();
            if (movies.Count() == 0)
            {
                APIworkerIMDB imdb = new APIworkerIMDB();
                var films = await imdb.GetTop250MoviesAsync();
                moviesInfos = _convertor.ConvertToMoviesInfo(films);
            }
            else
            {
                //moviesInfos = _convertor.ConvertToMoviesInfo(movies);
            }
            var topImdbViewModel = new TopImdbViewModel()
            {
                MoviePageList = moviesInfos.ToPagedList(pageNumber, pageSize),
                Movies = moviesInfos,
            };
            var onePageOfMovies = moviesInfos.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfTopMovies = onePageOfMovies;
            return View(topImdbViewModel);
        }

        public async Task<ActionResult> Search()
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1)
        {
            //var moviesInfos = new List<IndexMediaViewModel>();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var moviesDtos = _movieService.Items
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            stopWatch.Stop();
            var result = stopWatch.Elapsed;

            //var movies = AutoMap.Mapper.Map<IEnumerable<MediaDto>, List<IndexMediaViewModel>>(moviesDtos);

            var movieViewModel = new MovieViewModel()
            {
                Movies = new List<IndexMediaViewModel>(),
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = 10
                }
            };

            if (Session["Movies"] != null)
            {
                movieViewModel.Movies = Session["Movies"] as List<IndexMediaViewModel>;
            }
            else
            {
                //movieViewModel.Movies = moviesInfos;
            }

            return View(movieViewModel);
        }

        //public async Task<ActionResult> Details(int filmid, string imdbId)
        //{
        //    MediaDto movie;
        //    if (filmid != 0)
        //    {
        //        movie = _movieService.GetItem(filmid);
        //    }
        //    else
        //    {
        //        APIworkerIMDB imdb = new APIworkerIMDB();
        //        var films = await imdb.MovieOrSeriesInfo(imdbId);
        //        //DBSeed dbSeed = new DBSeed();
        //        //movie = dbSeed.MapMovieTWebDtotoDtoFromImdb(films);
        //        //movie = new MediaWebDto()
        //        //{
        //        //    Title = films.Title,
        //        //    Year = films.Year,
        //        //    PosterUrl = films.Image,
        //        //    Plot = films.Plot,
        //        //    ImdbRating = typesConvertor.StringToDecimal(films.IMDbRating)
        //        //};
        //    }
        //    return View(movie);
        //}

        public async Task<ActionResult> TopMovieDetails(int filmid, string imdbId)
        {
            MediaDto movie;
            if (filmid != 0)
            {
                movie = _movieService.GetItem(filmid);
            }
            else
            {
                APIworkerIMDB imdb = new APIworkerIMDB();
                var films = await imdb.MovieOrSeriesInfo(imdbId);
                DBSeed dbSeed = new DBSeed();
                movie = new MediaDto();
                //movie = dbSeed.MapMovieTWebDtotoDtoFromImdb(films);
                //movie = new MediaWebDto()
                //{
                //    Title = films.Title,
                //    Year = films.Year,
                //    PosterUrl = films.Image,
                //    Plot = films.Plot,
                //    ImdbRating = typesConvertor.StringToDecimal(films.IMDbRating)
                //};
            }
            return View("TopMovieDetails", movie);
        }

        public async Task<ActionResult> FilterInclude(string action, int countryId)
        {

            List<MediaDto> newMovies = new List<MediaDto>();
            if (Session["Movies"] != null)
            {
                newMovies = Session["Movies"] as List<MediaDto>;
            }

            var updatedmovies = newMovies.Select(m => m.Countries.Where(p => p.Id == countryId));
            //foreach (var m in movies)
            //{
            //    foreach (var c in m.Countries)
            //    {   
            //        if(c.Id==countryId)
            //            newMovies.Add(m);
            //    }
            //}

            Session["Movies"] = updatedmovies;
            //List<Media> newMovies = movies.Where(p => countries.All(p2=>p2.Id==countryId)).ToList();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> FilterExclude(int countryId)
        {

            List<MediaDto> newMovies = new List<MediaDto>();
            if (Session["Movies"] != null)
            {
                newMovies = Session["Movies"] as List<MediaDto>;
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
            //List<Media> newMovies = movies.Where(p => countries.All(p2=>p2.Id==countryId)).ToList();
            return RedirectToAction("Index");
        }
    }
}

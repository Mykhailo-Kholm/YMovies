using IMDbApiLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Services.Service;
using YMovies.Web.IMDB;
using YMovies.Web.IMDB.DBWorker;
using YMovies.Web.Models.MoviesInfoViewModel;
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
        }

        private const int pageSize = 4;
        private readonly IService<MediaDto> _movieService;

        private static MoviesContext context = new MoviesContext();
        private LikesService service = new LikesService(context);
        private static ISearchRepository repository = new MovieRepository(context);
        private static SearchService searchService = new SearchService(repository);

        public async Task<ActionResult> Like(int id)
        {
           service.LikeMedia(id);
           return RedirectToAction("Details", id);
        }

        public async Task<ActionResult> DisLike(int id)
        {
            service.DislikeMedia(id);
            return RedirectToAction("Details", id);
        }

        public async Task<ActionResult> Watched(int id)
        {
            service.DislikeMedia(id);
            return RedirectToAction("Details", id);
        }

        public async Task<ActionResult> MostLiked(int page = 1)
        {
            var films = _movieService.Items.OrderByDescending(m => m.NumberOfLikes);
            var moviesDtos = films
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var movies = AutoMap.Mapper.Map<IEnumerable<MediaDto>, List<IndexMediaViewModel>>(moviesDtos);

            var movieViewModel = new MovieViewModel()
            {
                Movies = movies,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = films.Count()
                }
            };
            return View("MostLiked", movieViewModel);
        }

        public async Task<ActionResult> MostWatched(int page = 1)
        {
            APIworkerIMDB imdb = new APIworkerIMDB();
            var films = await imdb.GetMostWatchedMovies();

            var movies = AutoMap.Mapper.Map<IEnumerable<MostPopularDataDetail>, List<IndexMediaViewModel>>(films);
            movies = movies
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var movieViewModel = new MovieViewModel()
            {
                Movies = movies,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = films.Count
                }
            };
            return View("MostWatched", movieViewModel);
        }

        public async Task<ActionResult> TopByIMDb(int page = 1)
        {
            var topmovies = _movieService.Items.OrderByDescending(m => m.ImdbRating).Take(250).ToList();

            if (topmovies.Count() == 0)
            {
                APIworkerIMDB imdb = new APIworkerIMDB();
                var films = await imdb.GetTop250MoviesAsync();
                topmovies = AutoMap.Mapper.Map<IEnumerable<Top250DataDetail>, List<MediaDto>>(films);
            }

            var moviesDtos = topmovies
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var movies = AutoMap.Mapper.Map<IEnumerable<MediaDto>, List<IndexMediaViewModel>>(moviesDtos);

            var movieViewModel = new MovieViewModel()
            {
                Movies = movies,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = topmovies.Count
                }
            };
            return View(movieViewModel);
        }

        public async Task<ActionResult> Search(string title, int page = 1)
        {
            var moviesDtos = searchService.GetMediaByTitle(title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var movies = AutoMap.Mapper.Map<IEnumerable<MediaDto>, List<IndexMediaViewModel>>(moviesDtos);

            var movieViewModel = new MovieViewModel()
            {
                Movies = movies,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = 10
                }
            };

            return View(movieViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1)
        {
            //var moviesInfos = new List<IndexMediaViewModel>();
            var moviesDtos = _movieService.Items
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var movies = AutoMap.Mapper.Map<IEnumerable<MediaDto>, List<IndexMediaViewModel>>(moviesDtos);

            var movieViewModel = new MovieViewModel()
            {
                Movies = movies,
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

        public async Task<ActionResult> Details(int filmid, string imdbId)
        {
            MediaDto movie;
            if (filmid != 0)
            {
                movie = _movieService.GetItem(filmid);
            }
            else
            {
                APIworkerIMDB imdb = new APIworkerIMDB();
                var film = await imdb.MovieOrSeriesInfoAsync(imdbId);
                DBSeed dbSeed = new DBSeed();
                movie = dbSeed.MapMovieDtoToDtoFromImdb(film);
            }
            return View(movie);
        }

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
                var films = await imdb.MovieOrSeriesInfoAsync(imdbId);
                DBSeed dbSeed = new DBSeed();
                movie = dbSeed.MapMovieDtoToDtoFromImdb(films);
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

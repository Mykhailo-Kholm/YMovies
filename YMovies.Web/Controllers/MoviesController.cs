using IMDbApiLib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly IService<MediaDto> _movieService;

        private static MoviesContext context = new MoviesContext();
        private LikesService service = new LikesService(context);
        private static ISearchRepository repository = new MovieRepository(context);
        private static SearchService searchService = new SearchService(repository);
        private static WatchService watchService = new WatchService(context);

        public async Task<ActionResult> Like(int id, string userId)
        {
            service.LikedMediaByUser(userId, id);
            return RedirectToAction("Details", new { filmId = id });
        }

        public async Task<ActionResult> DisLike(int id, string userId)
        {
            service.DislikedMediaByUser(userId, id);
            return RedirectToAction("Details", new { filmId = id });
        }

        public async Task<ActionResult> Watched(int id, string userId)
        {
            watchService.WatchedMediaByUser(userId, id);
            return RedirectToAction("Details", new { filmId = id });
        }

        public async Task<ActionResult> MostLiked(int page = 1)
        {
            var films = _movieService.Items.OrderByDescending(m => m.NumberOfLikes);
            var moviesDtos = films
                .Skip((page - 1) * PaginationInfo.ItemsPerPage)
                .Take(PaginationInfo.ItemsPerPage)
                .ToList();

            var movies = AutoMapperWeb.Mapper.Map<IEnumerable<MediaDto>, List<IndexMediaViewModel>>(moviesDtos);
            var movieViewModel = new MovieViewModel()
            {
                Movies = movies,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    TotalItems = films.Count()
                }
            };
            return View("MostLiked", movieViewModel);
        }

        public async Task<ActionResult> MostWatched(int page = 1)
        {
            APIworkerIMDB imdb = new APIworkerIMDB();
            var films = await imdb.GetMostWatchedMovies();

            var movies = AutoMapperWeb.Mapper.Map<IEnumerable<MostPopularDataDetail>, List<IndexMediaViewModel>>(films);
            movies = movies
                .Skip((page - 1) * PaginationInfo.ItemsPerPage)
                .Take(PaginationInfo.ItemsPerPage)
                .ToList();

            var movieViewModel = new MovieViewModel()
            {
                Movies = movies,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    TotalItems = films.Count
                }
            };
            return View("MostWatched", movieViewModel);
        }

        public async Task<ActionResult> TopByIMDb(int page = 1)
        {

            APIworkerIMDB imdb = new APIworkerIMDB();
            var films = await imdb.GetTop250MoviesAsync();
            var topmovies = AutoMapperWeb.Mapper.Map<IEnumerable<Top250DataDetail>, List<MediaDto>>(films);


            var moviesDtos = topmovies
                .Skip((page - 1) * PaginationInfo.ItemsPerPage)
                .Take(PaginationInfo.ItemsPerPage)
                .ToList();

            var movies = AutoMapperWeb.Mapper.Map<IEnumerable<MediaDto>, List<IndexMediaViewModel>>(moviesDtos);

            var movieViewModel = new MovieViewModel()
            {
                Movies = movies,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    TotalItems = topmovies.Count
                }
            };
            return View(movieViewModel);
        }

        public async Task<ActionResult> Search(string title, int page = 1)
        {
            var moviesDtos = searchService.GetMediaByTitle(title)
                .Skip((page - 1) * PaginationInfo.ItemsPerPage)
                .Take(PaginationInfo.ItemsPerPage)
                .ToList();

            var movies = AutoMapperWeb.Mapper.Map<IEnumerable<MediaDto>, List<IndexMediaViewModel>>(moviesDtos);

            var movieViewModel = new MovieViewModel()
            {
                Movies = movies,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    TotalItems = movies.Count
                }
            };

            return View(movieViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Index(FilterInfoDto filterModel, int page = 1)
        {
            var mediadtos = searchService
                .GetMediaByParams(filterModel);

            var moviesDtos = mediadtos
                .Skip((page - 1) * PaginationInfo.ItemsPerPage)
                .Take(PaginationInfo.ItemsPerPage)
                .ToList();

            var movies = AutoMapperWeb.Mapper.Map<IEnumerable<MediaDto>, List<IndexMediaViewModel>>(moviesDtos);

            var movieViewModel = new MovieViewModel()
            {
                Movies = movies,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    TotalItems = mediadtos.Count
                }
            };

            return View(movieViewModel);
        }

        public async Task<ActionResult> Details(int filmId, string imdbId)
        {
            MediaDto movie;
            var imdb = new APIworkerIMDB();

            if (filmId != 0)
            {
                movie = _movieService.GetItem(filmId);
                Session["Trailer"] = await imdb.GetYoutubeTrailerVideoID(imdbId);
            }
            else
            {
                var film = await imdb.MovieOrSeriesInfoAsync(imdbId);
                var dbSeed = new DBSeed();
                Session["Trailer"] = await imdb.GetYoutubeTrailerVideoID(imdbId);
                movie = dbSeed.MapMovieDtoToDtoFromImdb(film);
            }
            var userId = AuthenticationManager.User.Identity.GetUserId();
            if (userId != null)
            {
                ViewBag.IsLiked = service.IsLiked(userId, filmId);
                ViewBag.IsDisliked = service.IsDisliked(userId, filmId);
                ViewBag.IsWatched = watchService.IsWatched(userId, filmId);
            }
            return View(movie);
        }

        public async Task<ActionResult> TopMovieDetails(int filmid, string imdbId)
        {
            MediaDto movie;
            var imdb = new APIworkerIMDB();
            if (filmid != 0)
            {
                movie = _movieService.GetItem(filmid);
                Session["Trailer"] = await imdb.GetYoutubeTrailerVideoID(imdbId);
            }
            else
            {
                var films = await imdb.MovieOrSeriesInfoAsync(imdbId);
                var dbSeed = new DBSeed();
                await dbSeed.AddMovieByImbdId(imdbId);
                Session["Trailer"] = await imdb.GetYoutubeTrailerVideoID(imdbId);
                movie = dbSeed.MapMovieDtoToDtoFromImdb(films);
            }

            var userId = AuthenticationManager.User.Identity.GetUserId();
            if (userId != null)
            {
                ViewBag.IsLiked = service.IsLiked(userId, filmid);
                ViewBag.IsDisliked = service.IsDisliked(userId, filmid);
                ViewBag.IsWatched = watchService.IsWatched(userId, filmid);
            }
            return View("TopMovieDetails", movie);
        }

        public async Task<ActionResult> FilterExclude(int countryId)
        {
            var newMovies = new List<MediaDto>();
            if (Session["Movies"] != null)
            {
                newMovies = Session["Movies"] as List<MediaDto>;
            }

            Session["Movies"] = newMovies;

            return RedirectToAction("Index");
        }
    }
}

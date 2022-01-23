using IMDbApiLib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
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
        public MoviesController(IService<MediaDto> movieService, LikesService service, ISearchService searchService,
            WatchService watchService, MovieRepository repository)
        {
            _movieService = movieService;
            _likeService = service;
            _searchService = searchService;
            _watchService = watchService;
            this.repository = repository;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly IService<MediaDto> _movieService;
        private readonly ISearchService _searchService;
        private readonly LikesService _likeService;
        private readonly WatchService _watchService;
        private readonly MovieRepository repository;


        public async Task<ActionResult> Like(int id, string userId)
        {
            _likeService.LikedMediaByUser(userId, id);
            return RedirectToAction("Details", new { filmId = id });
        }

        public async Task<ActionResult> DisLike(int id, string userId)
        {
            _likeService.DislikedMediaByUser(userId, id);
            return RedirectToAction("Details", new { filmId = id });
        }

        public async Task<ActionResult> Watched(int id, string userId)
        {
            _watchService.WatchedMediaByUser(userId, id);
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
            var moviesDtos = _searchService.GetMediaByTitle(title)
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
        public async Task<ActionResult> Index(FilterInfoDto filterInfo, int page = 1)
        {
            var mediaDtos = _searchService
                     .GetMediaByParams(filterInfo);
            
            var moviesDtos = mediaDtos
                .Skip((page - 1) * PaginationInfo.ItemsPerPage)
                .Take(PaginationInfo.ItemsPerPage)
                .ToList();

            var movies = AutoMapperWeb.Mapper.Map<IEnumerable<MediaDto>, List<IndexMediaViewModel>>(moviesDtos);

            var movieViewModel = new MovieViewModel
            {
                Movies = movies,
                Pagination = new PaginationInfo
                {
                    CurrentPage = page,
                    TotalItems = mediaDtos.Count()
                },
                Filter = filterInfo
            };

            ViewData["SelectedCountries"] = filterInfo.Countries;
            ViewData["SelectedGenres"] = filterInfo.Genres;
            ViewData["SelectedTypes"] = filterInfo.Types;
            ViewData["SelectedYears"] = filterInfo.Years;
            return View(movieViewModel);
        }

        public async Task<ActionResult> Details(int filmId, string imdbId)
        {
            MediaDto movie;
            var imdb = new APIworkerIMDB();

            await AddTrailerForMedia(imdbId);

            if (filmId != 0)
            {
                movie = _movieService.GetItem(filmId);
            }
            else
            {
                var film = await imdb.MovieOrSeriesInfoAsync(imdbId);
                var dbSeed = new DBSeed();
                movie = await dbSeed.MapMovieDtoToDtoFromImdb(film);
            }
            var userId = AuthenticationManager.User.Identity.GetUserId();
            if (userId != null)
            {
                ViewBag.IsLiked = _likeService.IsLiked(userId, filmId);
                ViewBag.IsDisliked = _likeService.IsDisliked(userId, filmId);
                ViewBag.IsWatched = _watchService.IsWatched(userId, filmId);
            }
            return View(movie);
        }

        public async Task<ActionResult> PopularMovieDetails(int filmid, string imdbId)
        {
            MediaDto movie;
            var imdb = new APIworkerIMDB();
            if (filmid != 0)
            {
                movie = _movieService.GetItem(filmid);
            }
            else
            {
                var films = await imdb.MovieOrSeriesInfoAsync(imdbId);
                var dbSeed = new DBSeed();
                await dbSeed.AddMovieByImbdId(imdbId);
                movie = await dbSeed.MapMovieDtoToDtoFromImdb(films);
            }

            var userId = AuthenticationManager.User.Identity.GetUserId();
            if (userId != null)
            {
                ViewBag.IsLiked = _likeService.IsLiked(userId, filmid);
                ViewBag.IsDisliked = _likeService.IsDisliked(userId, filmid);
                ViewBag.IsWatched = _watchService.IsWatched(userId, filmid);
            }
            return View("PopularMovieDetails", movie);
        }

          public async Task<ActionResult> TopMovieDetails(int filmid, string imdbId)
        {
            MediaDto movie;
            var imdb = new APIworkerIMDB();
            if (filmid != 0)
            {
                movie = _movieService.GetItem(filmid);
            }
            else
            {
                var films = await imdb.MovieOrSeriesInfoAsync(imdbId);
                var dbSeed = new DBSeed();
                await dbSeed.AddMovieByImbdId(imdbId);
                movie = await dbSeed.MapMovieDtoToDtoFromImdb(films);
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
      
        private async Task AddTrailerForMedia(string idImdb)
        {
            if(string.IsNullOrEmpty(idImdb))
                return;

            string tempStrTrailerUrl;

            var media = repository.GetItem(idImdb);

            if (string.IsNullOrEmpty(media.TrailerUrl))
            {
                var imdb = new APIworkerIMDB();

                try
                {
                    tempStrTrailerUrl = await imdb.GetYoutubeTrailerVideoID(idImdb);
                }
                catch (NullReferenceException e)
                {
                    tempStrTrailerUrl = "https://www.youtube.com/embed/";
                }
                
                Session["Trailer"] = tempStrTrailerUrl;

                media.TrailerUrl = tempStrTrailerUrl;

                repository.UpdateItem(media);
            }

        }
    }
}

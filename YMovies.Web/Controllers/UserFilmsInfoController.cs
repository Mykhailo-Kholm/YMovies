using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.Service;
using YMovies.Web.Models.MoviesInfoViewModel;
using YMovies.Web.Utilites.Pagination;
using YMovies.Web.Utilities;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers
{
    public class UserFilmsInfoController : Controller
    {

        public UserFilmsInfoController()
        {
            _userService = new UserService();
        }
        private readonly UserService _userService;
        public ActionResult LikedMedias(string id, int page = 1)
        {
            var userLikedMedia = _userService.GetItem(id).LikedMovies;
            if (userLikedMedia == null) return View("EmptyInfo");
            var moviesDtos = userLikedMedia
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
                    TotalItems = userLikedMedia.Count()
                }
            };
            return View("LikedMedias", movieViewModel);
        }
        public ActionResult WatchedMedias(string id, int page = 1)
        {
            var userWatchedMedias = _userService.GetItem(id).WatchedMovies;
            if (userWatchedMedias == null) return View("EmptyInfo");
            var moviesDtos = userWatchedMedias
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
                    TotalItems = userWatchedMedias.Count()
                }
            };
            return View("WatchedMedias", movieViewModel);
        }
    }
}
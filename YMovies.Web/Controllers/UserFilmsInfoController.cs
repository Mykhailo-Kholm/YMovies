using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
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
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly UserService _userService;
        public ActionResult LikedMedias(int page = 1)
        {
            var id = AuthenticationManager.User.Identity.GetUserId();
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
        public ActionResult WatchedMedias(int page = 1)
        {
            var id = AuthenticationManager.User.Identity.GetUserId();
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
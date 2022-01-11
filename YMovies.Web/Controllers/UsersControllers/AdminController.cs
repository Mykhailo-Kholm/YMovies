﻿using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ymovies.Identity.BLL.DTO;
using Ymovies.Identity.BLL.Interfaces;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
using YMovies.Web.Models.AdminViewModels;
using YMovies.Web.Utilities;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers
{

    public class AdminController : Controller
    {
        readonly IService<MediaDto> _moviesService;
        readonly IService<CastDto> _castsService;
        readonly IService<TypeDto> _typesService;

        public AdminController(IService<MediaDto> moviesService, IService<CastDto> castsService, IService<TypeDto> typesService)
        {
            _moviesService = moviesService;
            _castsService = castsService;
            _typesService = typesService;
        }

        public IIdentityUserService UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IIdentityUserService>();
            }
        }

        public ActionResult Find()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FindUserViewModel findModel)
        {
            if (!ModelState.IsValid)
                return View("Find", findModel);
            var user = UserManager.GetUserByEmail(findModel.Email);
            var model = AutoMap.Mapper.Map<UserDTO, ManageUserRightsViewModel>(user);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmEdit(string email, bool? adminRights)
        {
            if (adminRights.Value)
                await UserManager.ChangeUserAdminRightsByEmail(email);

            return RedirectToAction("Index", "Home", null);
        }

        [HttpGet]
        public ActionResult CreateFilm()
        {
            ViewBag.Types = _typesService.Items.ToList();
            return View("FilmCreation", new NewFilmViewModel());
        }

        [HttpPost]
        public ActionResult CreateFilm(NewFilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = _typesService.Items.ToList();
                return View("FilmCreation", model);
            }

            UpdateFields(model);
            var mediaDto = AutoMap.Mapper.Map<MediaDto>(model);
            mediaDto.Cast = GetAllActors(model.Cast);
            _moviesService.AddItem(mediaDto);
            return RedirectToAction("Index", "Home");
        }

        private ICollection<CastDto> GetAllActors(ICollection<CastViewModel> castsModel)
            => castsModel.Select(m => _castsService.GetItem(m.Id)).ToList() ?? null;

        private void UpdateFields(NewFilmViewModel model)
        {
            model.Cast = UpdateFields(model.Cast);
            model.Country = UpdateFields(model.Country);
            model.Genre = UpdateFields(model.Genre);
        }

        private ICollection<CastViewModel> UpdateFields(ICollection<CastViewModel> cast) =>
                 cast != null ? cast.Where(c => c.Id != 0).ToList() : null;
        private ICollection<GenreDto> UpdateFields(ICollection<GenreDto> genres) =>
                    genres != null ? genres.Where(c => c.Id != 0).ToList() : null;
        private ICollection<CountryDto> UpdateFields(ICollection<CountryDto> countries) =>
                    countries != null ? countries.Where(c => c.Id != 0).ToList() : null;
    }
}
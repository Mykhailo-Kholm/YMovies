using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Utilities;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers.EntitiesContollers
{
    [Authorize(Roles = "admin")]
    public class MediaController : Controller
    {
        public MediaController(IService<MediaDto> moviesService, IService<CastDto> castsService, IService<TypeDto> typesService)
        {
            _moviesService = moviesService;
            _castsService = castsService;
            _typesService = typesService;
        }

        readonly IService<MediaDto> _moviesService;
        readonly IService<CastDto> _castsService;
        readonly IService<TypeDto> _typesService;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFilm(NewFilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = _typesService.Items.ToList();
                return View("CreateFilm", model);
            }

            UpdateFields(model);
            var mediaDto = AutoMap.Mapper.Map<MediaDto>(model);
            mediaDto.Cast = GetAllActors(model.Cast);
            _moviesService.AddItem(mediaDto);
            return RedirectToAction("Index", "Home");
        }
                
        public ActionResult Edit(int id)
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            return View();
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
           
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

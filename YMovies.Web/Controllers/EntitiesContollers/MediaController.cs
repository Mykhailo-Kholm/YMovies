using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
using YMovies.Web.Models.MediaCreationViewModels;
using YMovies.Web.Utilities;
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

        public ActionResult CreateMedia()
        {
            return View();
        }

        public ActionResult CreateSeries()
        {
            ViewBag.Types = _typesService.Items;
            return View("CreateSeries", new NewSeriesViewModel());
        }

        [HttpPost]
        public ActionResult CreateSeries(NewSeriesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = _typesService.Items;
                return View("CreateSeries", model);
            }
            UpdateFields(model);
            var mediaDto = AutoMapperWeb.Mapper.Map<MediaDto>(model);
            mediaDto.Cast = GetAllActors(model.Cast);
            mediaDto.Type = GetType(model.Type);
            mediaDto.ImdbId = "";
            _moviesService.AddItem(mediaDto);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateFilm()
        {
            ViewBag.Types = _typesService.Items;
            return View("CreateMovie", new NewFilmViewModel());
        }

        [HttpPost]
        public ActionResult CreateFilm(NewFilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = _typesService.Items;
                return View("CreateMovie", model);
            }

            UpdateFields(model);
            var mediaDto = AutoMapperWeb.Mapper.Map<MediaDto>(model);
            mediaDto.Type = GetType(model.Type);
            mediaDto.ImdbId = "";
            mediaDto.Cast = GetAllActors(model.Cast);
            _moviesService.AddItem(mediaDto);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditMovie(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var movie = _moviesService.GetItem(id.Value);
            if (movie == null)
                return HttpNotFound();
            ViewBag.Types = _typesService.Items.ToList();
            if (movie.Seasons != null)
            {
                var modelSeries = AutoMapperWeb.Mapper.Map<MediaDto, NewSeriesViewModel>(movie);
                return View("EditSeries", modelSeries);
            }
            var model = AutoMapperWeb.Mapper.Map<MediaDto, NewFilmViewModel>(movie);
            return View("EditMovie", model);
        }

        [HttpPost]
        public ActionResult EditSeries(NewSeriesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = _typesService.Items.ToList();
                return View("EditSeries", model);
            }
            UpdateFields(model);
            var mediaDto = AutoMapperWeb.Mapper.Map<NewSeriesViewModel, MediaDto>(model);
            mediaDto.Type = GetType(model.Type);
            mediaDto.Cast = GetAllActors(model.Cast);
            mediaDto.ImdbId = "";
            _moviesService.AddItem(mediaDto);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult EditMovies(NewFilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = _typesService.Items.ToList();
                return View("EditMovie", model);
            }
            UpdateFields(model);
            var mediaDto = AutoMapperWeb.Mapper.Map<NewFilmViewModel, MediaDto>(model);
            mediaDto.Type = GetType(model.Type);
            mediaDto.Cast = GetAllActors(model.Cast);
            mediaDto.ImdbId = "";
            _moviesService.AddItem(mediaDto);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var dto = _moviesService.GetItem(id.Value);
            if (dto.Seasons != null)
            {
                var modelsSeries = AutoMapperWeb.Mapper.Map<MediaDto, NewSeriesViewModel>(dto);
                return View("DeleteSeries", modelsSeries);
            }
            var model = AutoMapperWeb.Mapper.Map<MediaDto, NewFilmViewModel>(dto);
            return View(model);
        }

        [HttpPost]
        public ActionResult ConfirmDelete(int? mediaId)
        {
            if (mediaId == null)
                return HttpNotFound();
            var movie = _moviesService.GetItem(mediaId.Value);
            _moviesService.DeleteItem(movie);
            return RedirectToAction("Index", "Home");
        }

        private ICollection<CastDto> GetAllActors(ICollection<CastViewModel> castsModel)
            => castsModel.Select(m => _castsService.GetItem(m.Id)).ToList() ?? null;

        private TypeDto GetType(string name)
            => _typesService.Items.Where(t => t.Name == name).FirstOrDefault();

        private void UpdateFields(NewSeriesViewModel model)
        {
            model.Cast = UpdateFields(model.Cast);
            model.Countries = UpdateFields(model.Countries);
            model.Genres = UpdateFields(model.Genres);
        }

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

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
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
            mediaDto.Cast = GetAllActors(model.Cast);
            _moviesService.AddItem(mediaDto);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var movie = _moviesService.GetItem(id.Value);
            if (movie.Type.Name.Equals("TVSeries"))
                return View();
            var model = AutoMapperWeb.Mapper.Map<MediaDto, NewFilmViewModel> (movie);
            return View("EditMovies", model);
        }

        [HttpPost]
        public ActionResult EditMovies(NewFilmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Types = _typesService.Items.ToList();
                return View("CreateFilm", model);
            }
            UpdateFields(model);
            var mediaDto = AutoMapperWeb.Mapper.Map<NewFilmViewModel, MediaDto>(model);
            mediaDto.Cast = GetAllActors(model.Cast);
            _moviesService.UpdateItem(mediaDto);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if(id == null)
                return HttpNotFound();
            var dto = _moviesService.GetItem(id.Value);
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

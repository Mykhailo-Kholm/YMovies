using System.Linq;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
using YMovies.Web.Utilites;

namespace YMovies.Web.Controllers.EntitiesContollers
{
    [Authorize(Roles = "admin")]
    public class GenreController : Controller
    {
        public GenreController(IService<GenreDto> genreService)
        {
            _genreService = genreService;
        }

        private IService<GenreDto> _genreService;

        [HttpGet]
        public string Genres(string query = null)
        {
            var genres = _genreService.Items;

            if (!string.IsNullOrWhiteSpace(query))
                genres = genres.Where(x => x.Name.Contains(query));

            return TypeConverter.ToJson(genres);
        }

        [HttpGet]
        public ActionResult Index() => View(_genreService.Items);

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(GenreDto model)
        {
            if (ModelState.IsValid)
            {
                _genreService.AddItem(model);
                return RedirectToAction("Index", "Genre");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var genre = _genreService.GetItem(id.Value);
            if (genre == null)
                return HttpNotFound();
            return View(genre);
        }

        [HttpPost]
        public ActionResult Edit(GenreDto model)
        {
            if (ModelState.IsValid)
            {
                _genreService.UpdateItem(model);
                return RedirectToAction("Index", "Genre");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var genre = _genreService.GetItem(id.Value);
            if (genre == null)
                return HttpNotFound();
            return View(genre);
        }

        [HttpPost]
        public ActionResult Delete(GenreDto model)
        {
            _genreService.DeleteItem(model);
            return RedirectToAction("Index", "Genre");
        }

    }
}
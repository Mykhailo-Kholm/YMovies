using System.Linq;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
using YMovies.Web.Utilites;

namespace YMovies.Web.Controllers.EntitiesContollers
{
    [Authorize(Roles = "admin")]
    public class CountryController : Controller
    {
        public CountryController(IService<CountryDto> countrieservice)
        {
            _countryService = countrieservice;
        }

        private IService<CountryDto> _countryService;

        [HttpGet]
        public string Countries(string query = null)
        {
            var countries = _countryService.Items;

            if (!string.IsNullOrWhiteSpace(query))
                countries = countries.Where(x => x.Name.Contains(query));

            return TypeConverter.ToJson(countries);
        }

        [HttpGet]
        public ActionResult Index() => View(_countryService.Items);

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(CountryDto model)
        {
            if (ModelState.IsValid)
            {
                _countryService.AddItem(model);
                RedirectToAction("Index", "Country");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var country = _countryService.GetItem(id.Value);
            if (country == null)
                return HttpNotFound();
            return View(country);
        }

        [HttpPost]
        public ActionResult Edit(CountryDto model)
        {
            if (ModelState.IsValid)
            {
                _countryService.UpdateItem(model);
                return RedirectToAction("Index", "Country");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var country = _countryService.GetItem(id.Value);
            if (country == null)
                return HttpNotFound();
            return View(country);
        }

        [HttpPost]
        public ActionResult Delete(CountryDto model)
        {
            _countryService.DeleteItem(model);
            return RedirectToAction("Index", "Country");
        }
    }
}
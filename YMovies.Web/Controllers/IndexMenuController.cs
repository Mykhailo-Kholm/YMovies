using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.Web.Controllers
{
    public class IndexMenuController : Controller
    {
        public IndexMenuController(IService<GenreDto> genreService, IService<TypeDto> typeService, IService<CountryDto> countryService, IService<MediaDto> mediaService)
        {
            _genreService = genreService;
            _typeService = typeService;
            _countryService = countryService;
            _mediaService = mediaService;
        }

        private readonly IService<GenreDto> _genreService;
        private readonly IService<TypeDto> _typeService;
        private readonly IService<CountryDto> _countryService;
        private readonly IService<MediaDto> _mediaService;

        public ActionResult Menu(IEnumerable<string> selectedCountries, IEnumerable<string> selectedTypes, IEnumerable<string> selectedGenres, IEnumerable<string> selectedYears)
        {
            var model = new FilterInfoDto
            {
                Genres = _genreService.Items.Select(g => g.Name).Distinct(),
                Types = _typeService.Items.Distinct().Select(g => g.Name),
                Countries = _countryService.Items.Distinct().Select(c => c.Name),
                Years = _mediaService.Items.OrderBy(x => x.Year).Select(x => x.Year).Distinct().ToList()
            };
            ViewData["SelectedCountries"] = selectedCountries;
            ViewData["SelectedGenres"] = selectedGenres;
            ViewData["SelectedTypes"] = selectedTypes;
            ViewData["SelectedYears"] = selectedYears;
            return PartialView(model);
        }
    }
}
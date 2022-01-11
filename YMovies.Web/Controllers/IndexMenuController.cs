using System.Linq;
using System.Web.Mvc;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Services.Service;
using YMovies.Web.Models.IndexPageViewModels;

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

        public IndexMenuController()
        {
            var dbContext = new MoviesContext();
            _genreService = new GenreService(new GenreRepository(dbContext));
            _typeService = new TypeService(new TypeRepository(dbContext));
            _countryService = new CountryService(new CountryRepository(dbContext));
            _mediaService = new MovieService(new MovieRepository(dbContext));
        }

        private readonly IService<GenreDto> _genreService;
        private readonly IService<TypeDto> _typeService;
        private readonly IService<CountryDto> _countryService;
        private readonly IService<MediaDto> _mediaService;

        public ActionResult Menu()
        {
            var model = new MenuViewModel
            {
                Genres = _genreService.Items.Distinct().ToList(),
                Types = _typeService.Items.Distinct().ToList(),
                Countries = _countryService.Items.Distinct().ToList(),
                Years = _mediaService.Items.OrderBy(x => x.Year).Select(x => x.Year).Distinct().ToList()
            };
            return PartialView(model);
        }
    }
}
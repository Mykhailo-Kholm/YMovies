using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YMovies.Web.DTOs;
using YMovies.Web.Services.IService;

namespace YMovies.Web.Controllers.Api
{
    public class GenresController : ApiController
    {
        private IService<GenreWebDto> _genresService;
        public GenresController()
        {
        }

        public GenresController(IService<GenreWebDto> genresService)
        {
            _genresService = genresService;
        }

        IEnumerable<GenreWebDto> tempData = new List<GenreWebDto>
        {
            new GenreWebDto
            {
                Id = 1,
                Name = "Detecti"
            },
            new GenreWebDto
            {
                Id = 2,
                Name = "Genr2"
            },
            new GenreWebDto
            {
                Id = 3,
                Name = "Thriller"
            },
        };

        public IEnumerable<GenreWebDto> GetGenres(string query = null)
        {
            //var resultList = _countriesService.Items.AsQueryable();
            var resultList = tempData;

            if (!string.IsNullOrWhiteSpace(query))
                resultList = resultList.Where(r => r.Name.Contains(query));

            return resultList.ToList();
        }
    }
}
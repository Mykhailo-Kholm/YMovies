using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.Web.Controllers.Api
{
    public class GenresController : ApiController
    {
        private IService<GenresDto> _genresService;
        public GenresController()
        {
        }

        public GenresController(IService<GenresDto> genresService)
        {
            _genresService = genresService;
        }

        IEnumerable<GenresDto> tempData = new List<GenresDto>
        {
            new GenresDto
            {
                Id = 1,
                Name = "Detecti"
            },
            new GenresDto
            {
                Id = 2,
                Name = "Genr2"
            },
            new GenresDto
            {
                Id = 3,
                Name = "Thriller"
            },
        };

        public IEnumerable<GenresDto> GetGenres(string query = null)
        {
            //var resultList = _countriesService.Items.AsQueryable();
            var resultList = tempData;

            if (!string.IsNullOrWhiteSpace(query))
                resultList = resultList.Where(r => r.Name.Contains(query));

            return resultList.ToList();
        }
    }
}
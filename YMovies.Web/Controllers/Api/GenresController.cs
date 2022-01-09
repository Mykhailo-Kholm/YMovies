using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YMovies.MovieDbService.DTOs;
using YMovies.Web.DTOs;
using YMovies.Web.Services.IService;

namespace YMovies.Web.Controllers.Api
{
    public class GenresController : ApiController
    {
        public GenresController(IService<GenreDto> genresService)
        {
            _genresService = genresService;
        }

        public GenresController()
        {
        }
        
        private IService<GenreDto> _genresService;

        IEnumerable<GenreDto> tempData = new List<GenreDto>
        {
            new GenreDto
            {
                Id = 1,
                Name = "Detecti"
            },
            new GenreDto
            {
                Id = 2,
                Name = "Genr2"
            },
            new GenreDto
            {
                Id = 3,
                Name = "Thriller"
            },
        };

        public IEnumerable<GenreDto> GetGenres(string query = null)
        {
            //var resultList = _countriesService.Items.AsQueryable();
            var resultList = tempData;

            if (!string.IsNullOrWhiteSpace(query))
                resultList = resultList.Where(r => r.Name.Contains(query));

            return resultList.ToList();
        }
    }
}
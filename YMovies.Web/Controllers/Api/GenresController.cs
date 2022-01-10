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
        
        public IEnumerable<GenreDto> GetGenres(string query = null)
        {
            var resultList = _genresService.Items.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                resultList = resultList.Where(r => r.Name.Contains(query));

            return resultList.ToList();
        }
    }
}
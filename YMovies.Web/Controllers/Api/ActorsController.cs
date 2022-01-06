using System.Collections.Generic;
using System.Web.Http;
using YMovies.MovieDbService.Models;

namespace YMovies.Web.Controllers.Api
{
    public class ActorsController : ApiController
    {
        private List<Cast> _casts = new List<Cast>()
        {
            new Cast()
            {
                Id = 1,
                Name = "First",
                Surname = "Actor"
            },
            new Cast()
            {
                Id = 2,
                Name = "Second",
                Surname = "Actor"
            },
            new Cast()
            {
                Id = 3,
                Name = "Third",
                Surname = "Actor"
            }
        };

        public IEnumerable<Cast> GetActors() => _casts;
    }
}

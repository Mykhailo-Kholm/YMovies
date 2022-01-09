using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Services.IService;
using YMovies.Web.Utilities;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers.Api
{
    public class ActorsController : ApiController
    {
        private IService<CastDto> _actorsService;

        public ActorsController()
        {
        }

        public ActorsController(IService<CastDto> actorsService)
        {
            this._actorsService = actorsService;
        }

        private List<CastDto> _casts = new List<CastDto>()
        {
            new CastDto()
            {
                Id = 1,
                Name = "First",
                Surname = "Actor"
            },
            new CastDto()
            {
                Id = 2,
                Name = "Second",
                Surname = "Actor"
            },
            new CastDto()
            {
                Id = 3,
                Name = "Third",
                Surname = "Actor"
            }
        };

        public IEnumerable<CastViewModel> GetActors(string query = null)
        {
            //var temp = _actorsService.Items.AsEnumerable();
            var temp = _casts;

            if (!string.IsNullOrWhiteSpace(query))
                temp = temp.Where(t => t.Name.Contains(query)).ToList();
            
            var listOfCasts = AutoMap.Mapper.Map<List<CastDto>, List<CastViewModel>>(temp);
            return listOfCasts;
        }
    }
}

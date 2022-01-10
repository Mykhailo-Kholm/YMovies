using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
using YMovies.Web.Utilities;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers.Api
{
    public class ActorsController : ApiController
    {

        public ActorsController(IService<CastDto> actorsService)
        {
            this._actorsService = actorsService;
        }

        public ActorsController()
        {
        }
        
        private IService<CastDto> _actorsService;

        public IEnumerable<CastViewModel> GetActors(string query = null)
        {
            var temp = _actorsService.Items.ToList();

            if (!string.IsNullOrWhiteSpace(query))
                temp = temp.Where(t => t.Name.Contains(query)).ToList();
            
            var listOfCasts = AutoMap.Mapper.Map<List<CastDto>, List<CastViewModel>>(temp);
            return listOfCasts;
        }
    }
}

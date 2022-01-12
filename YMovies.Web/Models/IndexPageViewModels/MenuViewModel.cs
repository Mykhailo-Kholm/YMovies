using System.Collections.Generic;
using YMovies.MovieDbService.DTOs;

namespace YMovies.Web.Models.IndexPageViewModels
{
    public class MenuViewModel
    {
        public ICollection<CountryDto> Countries { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
        public ICollection<TypeDto> Types { get; set; }
        public ICollection<string> Years { get; set; }
    }
}
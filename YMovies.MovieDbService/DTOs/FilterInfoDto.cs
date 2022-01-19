using System.Collections.Generic;

namespace YMovies.MovieDbService.DTOs
{
    public class FilterInfoDto
    {
        public IEnumerable<string> Years { get; set; }
        public IEnumerable<string> Types { get; set; }
        public IEnumerable<string> Genres { get; set; }
        public IEnumerable<string> Countries { get; set; }

    }
}

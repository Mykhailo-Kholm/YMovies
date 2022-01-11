using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YMovies.Web.DTOs;

namespace YMovies.Web.TempModels
{
    public class MoviesInfo
    {
        public int Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public decimal ImdbRating { get; set; }
        public string PosterUrl { get; set; }
        public virtual ICollection<GenreWebDto> Genres { get; set; }
        public virtual ICollection<string> Years { get; set; }
        public virtual ICollection<CountryWebDto> Countries { get; set; }
        public virtual ICollection<Type> Types { get; set; }
    }
}
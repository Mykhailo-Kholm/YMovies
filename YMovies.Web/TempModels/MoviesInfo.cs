using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMovies.Web.TempModels
{
    public class MoviesInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal ImdbRating { get; set; }
        public string PosterUrl { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        public virtual ICollection<string> Years { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Type> Types { get; set; }
    }
}
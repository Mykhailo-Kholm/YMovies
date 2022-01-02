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
        //public string PosterUrl { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
        //public List<Movie> Movies { get; set; }//id poster rating genre
    }
}
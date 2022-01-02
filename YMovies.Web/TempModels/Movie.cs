using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMovies.Web.TempModels
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string ImdbId { get; set; }
        public string PosterUrl { get; set; }
        public string BoxOffice { get; set; }
        public string Title { get; set; }
        public decimal ImdbRating { get; set; }
        public int UsersRating { get; set; }
        public string Plot { get; set; }
        public string Type { get; set; }
     
        public string Year { get; set; }
        public decimal Budget { get; set; }

        public virtual ICollection<Cast> Cast { get; set; }
        public virtual ICollection<Country> Countries { get; set; }
        public virtual ICollection<Genre> Genres { get; set; }
    }
}
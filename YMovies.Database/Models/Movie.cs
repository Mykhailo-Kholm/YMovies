using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMovies.Database.Models
{
    class Movie
    {
        public int MovieId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string Year { get; set; }
        public int CountryId { get; set; }
        public int CastId { get; set; }
        public string Plot { get; set; }
        public decimal Budget { get; set; }
        public string BoxOffice { get; set; }
        public decimal ImdbRating { get; set; }
        public int statisticId { get; set; }
    }
}

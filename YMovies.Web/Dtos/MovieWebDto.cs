using System.Collections.Generic;

namespace YMovies.Web.DTOs
{
    public class MovieWebDto
    {
        public int MovieId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string Year { get; set; }
        public string Plot { get; set; }
        public decimal Budget { get; set; }
        public string BoxOffice { get; set; }
        public decimal ImdbRating { get; set; }
        public decimal Rating { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public string Type { get; set; }
        public ICollection<CastWebDto> Cast { get; set; }
        public ICollection<CountryWebDto> Countries { get; set; }
        public ICollection<GenreWebDto> Genres { get; set; }
        public bool IsWatched { get; set; }
    }
}

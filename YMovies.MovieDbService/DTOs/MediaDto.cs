using System.Collections.Generic;

namespace YMovies.MovieDbService.DTOs
{
    public class MediaDto
    {
        public int MediaId { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string PosterUrl { get; set; }
        public string Year { get; set; }
        public string Plot { get; set; }
        public decimal Budget { get; set; }
        public string GlobalFees { get; set; }
        public string WeekFees { get; set; }
        public string Companies { get; set; }
        public decimal ImdbRating { get; set; }
        public decimal Rating { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
        public string Type { get; set; }
        public ICollection<SeasonDto> Seasons { get; set; }
        public ICollection<CastDto> Cast { get; set; }
        public ICollection<CountryDto> Countries { get; set; }
        public ICollection<GenreDto> Genres { get; set; }
    }
}

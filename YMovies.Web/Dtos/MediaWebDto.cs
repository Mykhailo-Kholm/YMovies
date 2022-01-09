using System.Collections.Generic;
using YMovies.Web.Dtos;

namespace YMovies.Web.DTOs
{
    public class MediaWebDto
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

        public TypeWebDto Type { get; set; }
        public virtual ICollection<UserWebDto> UsersLiked { get; set; }
        public virtual ICollection<UserWebDto> UsersWatched { get; set; }
        public virtual ICollection<SeasonWebDto> Seasons { get; set; }
        public virtual ICollection<CastWebDto> Cast { get; set; }
        public virtual ICollection<CountryWebDto> Countries { get; set; }
        public virtual ICollection<GenreWebDto> Genres { get; set; }
    }
}

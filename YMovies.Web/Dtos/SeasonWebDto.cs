using YMovies.Web.Dtos;

namespace YMovies.Web.DTOs
{
    public class SeasonWebDto
    {
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int NumberOfEpisodes { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }

        public int CurrentSeriesId { get; set; }
        public MediaWebDto CurrentSeries { get; set; }
        //public virtual ICollection<UserWebDto> UsersLiked { get; set; }
        //public virtual ICollection<UserWebDto> UsersWatched { get; set; }
    }
}

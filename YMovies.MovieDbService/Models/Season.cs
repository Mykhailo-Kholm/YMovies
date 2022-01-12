using System.Collections.Generic;

namespace YMovies.MovieDbService.Models
{
    public class Season
    {
        public int SeasonId { get; set; }
        public string Name { get; set; }
        public int NumberOfEpisodes { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }

        public int CurrentSeriesId { get; set; }
        public Media CurrentSeries { get; set; }
        public virtual ICollection<User> UsersLiked { get; set; }
        public virtual ICollection<User> UsersWatched { get; set; }
    }
}

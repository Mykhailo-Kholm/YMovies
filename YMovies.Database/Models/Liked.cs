using System.Collections.Generic;

namespace YMovies.Database.Models
{
    public class Liked
    {
        public int LikedId { get; set; }
        public User User { get; set; }
        public ICollection<Movie> LikedMovies { get; set; }
        public ICollection<Season> LikedSeasons { get; set; }
    }
}

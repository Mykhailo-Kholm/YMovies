using System.Collections.Generic;

namespace YMovies.Database.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<Movie> LikedMovies { get; set; }
        public ICollection<Season> LikedSeasons { get; set; }
        public ICollection<Movie> WatchedMovies { get; set; }
        public ICollection<Season> WatchedSeasons { get; set; }
    }
}

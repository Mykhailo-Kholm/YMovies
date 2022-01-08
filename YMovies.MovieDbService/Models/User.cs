using System.Collections.Generic;

namespace YMovies.MovieDbService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<Media> LikedMedias { get; set; }
        public ICollection<Season> LikedSeasons { get; set; }
        public ICollection<Media> WatchedMedias { get; set; }
        public ICollection<Season> WatchedSeasons { get; set; }
    }
}

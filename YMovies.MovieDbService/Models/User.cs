using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YMovies.MovieDbService.Models
{
    public class User
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
        public string FullName { get; set; }
        public ICollection<Media> LikedMedias { get; set; } = new List<Media>();
        public ICollection<Season> LikedSeasons { get; set; }
        public ICollection<Media> WatchedMedias { get; set; }
        public ICollection<Season> WatchedSeasons { get; set; }
    }
}

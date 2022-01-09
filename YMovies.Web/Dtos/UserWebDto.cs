using System.Collections.Generic;
using YMovies.Web.DTOs;

namespace YMovies.Web.Dtos
{
    public class UserWebDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<MediaWebDto> LikedMovies { get; set; }
        public ICollection<SeasonWebDto> LikedSeasons { get; set; }
        public ICollection<MediaWebDto> WatchedMovies { get; set; }
        public ICollection<SeasonWebDto> WatchedSeasons { get; set; }
    }
}
using System.Collections.Generic;
using YMovies.Web.DTOs;

namespace YMovies.Web.Dtos
{
    public class UserWebDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<MovieWebDto> LikedMovies { get; set; }
        public ICollection<SeasonWebDto> LikedSeasons { get; set; }
        public ICollection<MovieWebDto> WatchedMovies { get; set; }
        public ICollection<SeasonWebDto> WatchedSeasons { get; set; }
    }
}
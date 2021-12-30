using System.Collections.Generic;

namespace YMovies.Database.DTOs
{
    class UserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public ICollection<MovieDto> LikedMovies { get; set; }
        public ICollection<SeasonDto> LikedSeasons { get; set; }
        public ICollection<MovieDto> WatchedMovies { get; set; }
        public ICollection<SeasonDto> WatchedSeasons { get; set; }
    }
}

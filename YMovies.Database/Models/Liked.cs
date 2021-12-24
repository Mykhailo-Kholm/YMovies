using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

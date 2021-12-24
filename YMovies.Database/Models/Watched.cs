using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMovies.Database.Models
{
    [Table("Watched")]
    public class Watched
    {
        public int WatchedId { get; set; }
        public User User { get; set; }
        public ICollection<Movie> WatchedMovies { get; set; }
        public ICollection<Season> WatchedSeasons { get; set; }
    }
}

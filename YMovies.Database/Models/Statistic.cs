using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMovies.Database.Models
{
    public class Statistic
    {
        public int Id { get; set; } 
        public decimal Rating { get; set; } 
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }
    }
}

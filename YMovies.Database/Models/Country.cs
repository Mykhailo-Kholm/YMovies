using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YMovies.Database.Models
{
    public class Country
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Series> Series { get; set; }
    }
}

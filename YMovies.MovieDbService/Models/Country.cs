using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YMovies.MovieDbService.Models
{
    public class Country
    {
        [Key] 
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Media> Medias { get; set; }
    }
}

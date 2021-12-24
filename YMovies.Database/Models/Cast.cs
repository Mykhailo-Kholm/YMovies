using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMovies.Database.Models
{
    public class Cast
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Surname { get; set; }
        public string PictureUrl { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<Series> Series { get; set; }
    }
}

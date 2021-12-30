using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YMovies.Database.Models
{
    class Type
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string Name { get; set; }
        public Movie Movie { get; set; }
    }
}

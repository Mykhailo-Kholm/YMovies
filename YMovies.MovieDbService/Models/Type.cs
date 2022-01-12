using System.ComponentModel.DataAnnotations;

namespace YMovies.MovieDbService.Models
{
    public class Type
    {
        [Key]
        public int Id { set; get; }
        [Required]
        public string Name { get; set; }
    }
}

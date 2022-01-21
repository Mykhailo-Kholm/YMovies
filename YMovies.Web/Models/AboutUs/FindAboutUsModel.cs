using System.ComponentModel.DataAnnotations;

namespace YMovies.Web.Models.AboutUs
{
    public class FindAboutUsModel
    {
        [Required(ErrorMessage = "Please, provide id(1-4)")]
        [Range(minimum: 1, maximum: 4)]
        public int Id { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace YMovies.Web.Models.AdminViewModels
{
    public class FindModel
    {
        [Required(ErrorMessage = "Please enter a email of worker")]
        public string Email { get; set; }
    }
}
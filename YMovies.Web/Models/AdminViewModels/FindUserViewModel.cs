using System.ComponentModel.DataAnnotations;
using YMovies.Web.Utilites.ValidationAttributes;

namespace YMovies.Web.Models.AdminViewModels
{
    public class FindUserViewModel
    {
        [Required(ErrorMessage = "Please, provide users email")]
        [IdentityUser(ErrorMessage = "This email doesn't belongs to any user")]
        public string Email { get; set; }
    }
}
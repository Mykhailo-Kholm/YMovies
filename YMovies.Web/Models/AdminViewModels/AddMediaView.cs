using System.ComponentModel.DataAnnotations;

namespace YMovies.Web.Models.AdminViewModels
{
    public class AddMediaView
    {
        [Required]
        [RegularExpression(@"^tt[0-9]{6,8}$", ErrorMessage = "input id like : tt(100001)-(10000001)")]
        public string ImdbId { get; set; }
    }
}
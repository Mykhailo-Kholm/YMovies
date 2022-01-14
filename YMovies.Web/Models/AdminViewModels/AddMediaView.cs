using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YMovies.Web.Models.AdminViewModels
{
    public class AddMediaView
    {
        [Required]
        [RegularExpression(@"^tt[0-9]{6,8}$")]
        public string ImdbId { get; set; }
    }
}
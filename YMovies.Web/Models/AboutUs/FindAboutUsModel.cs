using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YMovies.Web.Models.AboutUs
{
    public class FindAboutUsModel
    {
        [Required(ErrorMessage = "Please, provide id(1-4)")]
        [Range(minimum: 1, maximum: 4)]
        public int Id { get; set; }
    }
}
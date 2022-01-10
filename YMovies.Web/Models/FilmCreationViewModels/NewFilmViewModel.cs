using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YMovies.MovieDbService.DTOs;
using YMovies.Web.Utilites.ValidationAttributes;

namespace YMovies.Web.ViewModels
{
    public class NewFilmViewModel
    {
        public int MovieId { get; set; }        
        
        [Required(ErrorMessage = "This field cannot be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        [DataType(DataType.ImageUrl, ErrorMessage = "This url isn't correct")]
        public string PosterUrl { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        [RegularExpression("(19[5-9][0-9])|(20|[0-1][0-9]|2[0-2])", ErrorMessage = "Year isn't correct")]
        public string Year { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public string Plot { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public string BoxOffice { get; set; }

        [Required]
        [RegularExpression("[0-9](,[0-9])?", ErrorMessage = "This value isn't correct, should be in format 0,0")]
        [Display(Name = "Rating on Imdb")]
        public decimal ImdbRating { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public string Type { get; set; }
        
        [Cast(ErrorMessage = "This field cannot be empty")]
        public ICollection<CastViewModel> Cast { get; set; }

        [Country(ErrorMessage = "This field cannot be empty")]
        public ICollection<CountryDto> Country { get; set; }

        [Genres(ErrorMessage = "This field cannot be empty")]
        public ICollection<GenreDto> Genre { get; set; }
    }
}
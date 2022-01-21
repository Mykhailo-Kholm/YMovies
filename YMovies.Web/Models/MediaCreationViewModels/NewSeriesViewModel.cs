using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using YMovies.MovieDbService.DTOs;
using YMovies.Web.Utilites.ValidationAttributes;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Models.MediaCreationViewModels
{
    public class NewSeriesViewModel
    {
        public int MediaId { get; set; }
        public string ImdbId { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        [DataType(DataType.ImageUrl, ErrorMessage = "This url isn't correct")]
        public string PosterUrl { get; set; }
        public string TrailerUrl { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        [RegularExpression("^[12][0-9]{3}$", ErrorMessage = "Year isn't correct")]
        public string Year { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public string Plot { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public string GlobalFees { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public string WeekFees { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public string Companies { get; set; }

        [Required]
        [RegularExpression("[0-9]([.][0-9][0-9])?", ErrorMessage = "This value isn't correct, should be in format 0,0")]
        [Display(Name = "Rating on Imdb")]
        public decimal ImdbRating { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        public string Type { get; set; }

        public ICollection<SeasonViewModel> Seasons { get; set; }

        [Cast(ErrorMessage = "This field cannot be empty")]
        public ICollection<CastViewModel> Cast { get; set; }

        [Country(ErrorMessage = "This field cannot be empty")]
        public ICollection<CountryDto> Countries { get; set; }

        [Genres(ErrorMessage = "This field cannot be empty")]
        public ICollection<GenreDto> Genres { get; set; }
    }
}
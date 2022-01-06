using System.Collections.Generic;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;

namespace YMovies.Web.ViewModels
{
    public class FilmCreationViewModel
    {
        public NewFilm Movie { get; set; }

        public SelectList Types { get; set; }

        public SelectList Casts { get; set; }
        
        public SelectList Countries { get; set; }

        public SelectList Genres { get; set; }         
    }
}
using System;
using System.Collections.Generic;
using PagedList;
using YMovies.Web.Dtos;
using YMovies.Web.DTOs;
using YMovies.Web.TempModels;

namespace YMovies.Web.ViewModels
{
    public class MovieViewModel
    {
        public List<MoviesInfo> MoviesInfo { get; set; }
        public IEnumerable<CountryWebDto> Countries { get; set; }
        public IEnumerable<GenreWebDto> Genres { get; set; }
        public IEnumerable<TypeWebDto> Types { get; set; }
        public List<string> Years { get; set; }
    }
}
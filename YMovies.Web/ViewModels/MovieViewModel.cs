using System;
using System.Collections.Generic;
using PagedList;
using YMovies.Web.DTOs;
using YMovies.Web.TempModels;

namespace YMovies.Web.ViewModels
{
    public class MovieViewModel
    {
        public IPagedList<MoviesInfo> MoviePageList { get; set; }
        public List<MoviesInfo> MoviesInfo { get; set; }
        public IEnumerable<CountryWebDto> Countries { get; set; }
        public List<GenreWebDto> Genres { get; set; }
        public List<Type> Types { get; set; }
        public List<string> Years { get; set; }
    }
}
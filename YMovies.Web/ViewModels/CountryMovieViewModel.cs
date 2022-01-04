using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using YMovies.Web.TempModels;

namespace YMovies.Web.ViewModels
{
    public class CountryMovieViewModel
    {
        public IPagedList<MoviesInfo> MoviePageList { get; set; }
        public List<MoviesInfo> MoviesInfo { get; set; }
        public List<Country> Countries { get; set; }
    }
}
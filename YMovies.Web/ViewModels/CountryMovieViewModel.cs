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
        public IPagedList<Movie> MoviePageList { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
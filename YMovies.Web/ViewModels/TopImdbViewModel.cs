using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMDbApiLib.Models;
using PagedList;
using YMovies.Web.TempModels;

namespace YMovies.Web.ViewModels
{
    public class TopImdbViewModel
    {
        public IPagedList<MoviesInfo> MoviePageList { get; set; }
        public List<MoviesInfo> Movies { get; set; }
    }
}
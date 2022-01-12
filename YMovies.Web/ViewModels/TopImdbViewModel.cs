using PagedList;
using System.Collections.Generic;
using YMovies.Web.TempModels;

namespace YMovies.Web.ViewModels
{
    public class TopImdbViewModel
    {
        public IPagedList<MoviesInfo> MoviePageList { get; set; }
        public List<MoviesInfo> Movies { get; set; }
    }
}
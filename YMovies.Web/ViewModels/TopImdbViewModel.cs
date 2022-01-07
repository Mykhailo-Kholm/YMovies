using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMDbApiLib.Models;
using PagedList;

namespace YMovies.Web.ViewModels
{
    public class TopImdbViewModel
    {
        public IPagedList<Top250DataDetail> MoviePageList { get; set; }
        public List<Top250DataDetail> Movies { get; set; }
    }
}
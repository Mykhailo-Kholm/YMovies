using System.Collections.Generic;
using YMovies.Web.Models.MoviesInfoViewModel;
using YMovies.Web.Utilites.Pagination;

namespace YMovies.Web.ViewModels
{
    public class MovieViewModel
    {
        public ICollection<IndexMediaViewModel> Movies { get; set; }

        public PaginationInfo Pagination { get; set; }
    }
}
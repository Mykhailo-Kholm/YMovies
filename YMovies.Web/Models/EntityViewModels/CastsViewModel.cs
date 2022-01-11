using System.Collections.Generic;
using YMovies.Web.Utilites.Pagination;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Models.EntityViewModels
{
    public class CastsViewModel
    {
        public IEnumerable<CastViewModel> Casts { get; set; }

        public PaginationInfo Pagination { get; set; }
    }
}
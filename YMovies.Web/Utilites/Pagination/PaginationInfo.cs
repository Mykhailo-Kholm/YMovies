using System;
namespace YMovies.Web.Utilites.Pagination
{
    public class PaginationInfo
    {        
        public const int ItemsPerPage = 7;
        public int TotalItems { get; set; }     
        public int CurrentPage { get; set; }
        public int TotalPages => 
            (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}

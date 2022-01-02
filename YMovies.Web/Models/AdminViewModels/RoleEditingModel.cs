using System.Web.Mvc;
using YMovies.Web.Dtos;

namespace YMovies.Web.Models.AdminViewModels
{
    public class RoleEditingModel
    {
        public string UserId { get; set; }       
        public string Email { get; set; }
        public SelectList Roles { get; set; }
    }
}
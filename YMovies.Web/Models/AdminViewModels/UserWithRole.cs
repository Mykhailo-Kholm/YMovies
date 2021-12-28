using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMovies.Web.Models.AdminViewModels
{
    public class UserWithRole
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
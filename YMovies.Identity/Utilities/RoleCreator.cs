using Microsoft.AspNet.Identity.EntityFramework;
using YMovies.Identity.Models;

namespace YMovies.Identity.Managers
{
    public class RoleCreator
    {
        public static ApplicationRole GetAdmin() => new ApplicationRole { Name = "admin" };
        public static ApplicationRole GetUser() => new ApplicationRole { Name = "user" };        
    }
}

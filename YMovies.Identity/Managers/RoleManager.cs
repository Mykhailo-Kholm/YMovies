using Microsoft.AspNet.Identity.EntityFramework;

namespace YMovies.Identity.Managers
{
    public class RoleManager
    {
        public static IdentityRole GetAdmin() => new IdentityRole { Name = "admin" };
        public static IdentityRole GetUser() => new IdentityRole { Name = "user" };        
    }
}

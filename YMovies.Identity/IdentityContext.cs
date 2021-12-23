using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using YMovies.Identity.Users;
using YMovies.Identity.Utilities;

namespace YMovies.Identity
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext()
            : base("name=IdentityDb") 
        {
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }
}

using Microsoft.AspNet.Identity.EntityFramework;
using YMovies.Identity.Models;

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

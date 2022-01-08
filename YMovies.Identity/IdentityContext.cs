using Microsoft.AspNet.Identity.EntityFramework;
using YMovies.Identity.DAL.Models;

namespace YMovies.Identity
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(string connectionString)
            : base(connectionString)
        {
        }

        public static IdentityContext Create()
        {
            return new IdentityContext("name=IdentityDb");
        }
    }
}

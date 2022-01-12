using Microsoft.AspNet.Identity;
using YMovies.Identity.DAL.Models;

namespace YMovies.Identity.DAL.Managers
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }
    }
}

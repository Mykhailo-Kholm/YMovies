using Microsoft.AspNet.Identity;
using YMovies.Identity.DAL.Models;

namespace YMovies.Identity.DAL.Managers
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store)
        { }
    }
}

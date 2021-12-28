using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using YMovies.Identity.Managers;
using YMovies.Identity.Models;

namespace YMovies.Identity.Utilities
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<IdentityContext>
    {
        protected override void Seed(IdentityContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            var adminRole = RoleCreator.GetAdmin();
            var userRole = RoleCreator.GetUser();

            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            var admin = new ApplicationUser
            {
                Name = "Petya",
                SecondName = "Pupkin",
                Email = "admin01@gmail.com",
                UserName = "admin01@gmail.com"
            };

            string password = "Admin01_pass";
            userManager.Create(admin, password);

            userManager.AddToRole(admin.Id, adminRole.Name);
               
            base.Seed(context);
        }
    }
}

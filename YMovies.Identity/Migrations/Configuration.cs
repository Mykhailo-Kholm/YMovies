namespace YMovies.Identity.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using YMovies.Identity.Managers;
    using YMovies.Identity.Users;

    internal sealed class Configuration : DbMigrationsConfiguration<YMovies.Identity.IdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "YMovies.Identity.IdentityContext";
        }

        protected override void Seed(YMovies.Identity.IdentityContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var adminRole = RoleManager.GetAdmin();
            var userRole = RoleManager.GetUser();

            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            context.Roles.AddOrUpdate(adminRole);
            context.Roles.AddOrUpdate(userRole);

            var admin = new ApplicationUser
            {
                Name = "Petya",
                SecondName = "Pupkin",
                Email = "admin01@gmail.com",
                UserName = "admin01@gmail.com"
            };

            string password = "Admin01_pass";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, adminRole.Name);
                userManager.AddToRole(admin.Id, userRole.Name);
            }

            context.Users.AddOrUpdate(admin);
        }
    }
}

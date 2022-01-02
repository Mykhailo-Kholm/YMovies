namespace YMovies.Identity.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using YMovies.Identity.Managers;
    using YMovies.Identity.Models;

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

            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            var adminRole = RoleCreator.GetAdmin();

            roleManager.Create(adminRole);

            context.Roles.AddOrUpdate(adminRole);

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

            context.Users.AddOrUpdate(admin);
        }
    }
}

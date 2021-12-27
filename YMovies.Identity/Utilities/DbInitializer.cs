﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using YMovies.Identity.Managers;
using YMovies.Identity.Users;

namespace YMovies.Identity.Utilities
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<IdentityContext>
    {
        protected override void Seed(IdentityContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var adminRole = RoleManager.GetAdmin();
            var userRole = RoleManager.GetUser();

            roleManager.Create(adminRole);
            roleManager.Create(userRole);

            var admin = new ApplicationUser
            {
                Name = "Petya",
                Surname = "Pupkin",
                Email = "somemail@mail.ru",
                UserName = "somemail@mail.ru"
            };

            string password = "A12_aaa";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, adminRole.Name);
                userManager.AddToRole(admin.Id, userRole.Name);
            }

            base.Seed(context);
        }
    }
}
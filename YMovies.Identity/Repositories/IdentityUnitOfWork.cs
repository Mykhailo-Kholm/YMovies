using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YMovies.Identity.DAL.Interfaces;
using YMovies.Identity.DAL.Managers;
using YMovies.Identity.DAL.Models;

namespace YMovies.Identity.DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        public IdentityUnitOfWork(string connectionString = null)
        {
            db = new IdentityContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
        }

        public ApplicationUserManager ApplicationUserManager
        {
            get { return userManager; }
        }
       
        public ApplicationRoleManager ApplicationRoleManager
        {
            get { return roleManager; }
        }

        private IdentityContext db;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private bool disposed = false;
        
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}

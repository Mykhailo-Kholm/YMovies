using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Ymovies.Identity.BLL.Interfaces;
using Ymovies.Identity.BLL.Services;

[assembly: OwinStartup(typeof(YMovies.Web.App_Start.Startup))]

namespace YMovies.Web.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IIdentityUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }

        private IIdentityUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("IdentityDb");
        }
    }
}
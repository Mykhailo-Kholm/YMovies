using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YMovies.Web.Startup))]
namespace YMovies.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using YMovies.Identity;
using YMovies.Identity.Utilities;
using YMovies.Web.Utilities;
using System.Web.Http;

namespace YMovies.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Database.SetInitializer<IdentityContext>(new DbInitializer());
           
            AutoMap.RegisterMapping();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

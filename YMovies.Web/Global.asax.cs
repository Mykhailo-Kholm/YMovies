using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YMovies.MovieDbService.Utilities;
using YMovies.Web.Utilities;

namespace YMovies.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMap.RegisterMapping();
            AutoMapperWeb.RegisterMapping();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

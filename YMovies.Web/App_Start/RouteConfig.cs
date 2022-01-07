using System.Web.Mvc;
using System.Web.Routing;

namespace YMovies.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "SerchByGenre",
                url: "Home/Genre/{genre}",
                defaults: new { controller = "Home", action = "Genre", genre = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "SerchByTitle",
                url: "Home/Title/{title}",
                defaults: new { controller = "Home", action = "Title", title = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

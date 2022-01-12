using System.Web.Optimization;

namespace YMovies.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-3.4.1.min.js",
                        "~/Scripts/typeahead.bundle.js",
                         "~/Scripts/jquery.validate*",
                         "~/Scripts/modernizr-*",
                         "~/Scripts/bootstrap.min.js"));

            bundles.Add(new StyleBundle("~/Content/libcss").Include(
                      "~/Content/site.css",
                      "~/Content/profiles.css",
                      "~/Content/typeahead.css"));
        }
    }
}

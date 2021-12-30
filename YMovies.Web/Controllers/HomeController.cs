using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using YMovies.Web.IMDB;

namespace YMovies.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles ="admin, user")]
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async Task<ActionResult> Mock(string id)
        {
            APIworkerIMDB imdb = new APIworkerIMDB();

            ViewData["MovieReport"] = await imdb.ReportForMovie(id);

            return View("MockFilm");
        }
    }
}
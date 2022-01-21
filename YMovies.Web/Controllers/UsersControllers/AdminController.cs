using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Ymovies.Identity.BLL.DTO;
using Ymovies.Identity.BLL.Interfaces;
using YMovies.Web.IMDB.DBWorker;
using YMovies.Web.Models.AboutUs;
using YMovies.Web.Models.AdminViewModels;
using YMovies.Web.Utilites;
using YMovies.Web.Utilities;

namespace YMovies.Web.Controllers
{

    public class AdminController : Controller
    {
       

        public IIdentityUserService UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IIdentityUserService>();
            }
        }
        
        [HttpGet]
        public string Users(string query = null)
        {
            var userDto = UserManager.GetAllUsers();
            var viewModels = AutoMapperWeb.Mapper.Map<IEnumerable<UserDTO>, List<FindUserViewModel>>(userDto);
            if (!string.IsNullOrWhiteSpace(query))
                viewModels = viewModels.Where(u => u.Email.Contains(query)).ToList();
            return TypeConverter.ToJson(viewModels);
        }

        public ActionResult Find()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(FindUserViewModel findModel)
        {
            if (!ModelState.IsValid)
                return View("Find", findModel);
            var user = UserManager.GetUserByEmail(findModel.Email);
            var model = AutoMapperWeb.Mapper.Map<UserDTO, ManageUserRightsViewModel>(user);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ConfirmEdit(string email, bool? adminRights)
        {
            if (adminRights.Value)
                await UserManager.ChangeUserAdminRightsByEmail(email);

            return RedirectToAction("Index", "Home", null);
        }
        [HttpGet]
        public ActionResult EditAboutUs()
        {
            return View("FindAboutUS");
        }

        [HttpPost]
        public ActionResult EditAboutUs(FindAboutUsModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("FindAboutUS");
            }

            Session["AboutUsId"] = model.Id - 1;

            string FilePath = Server.MapPath("~/Json/");
            string fileName = "aboutus.json";
            var str = System.IO.File.ReadAllText(FilePath + fileName);

            AboutUsViewModel infoList = new JavaScriptSerializer().Deserialize<AboutUsViewModel>(str);

            var aboutUS = infoList.aboutUs[model.Id - 1];

            return View("EditAboutUS", aboutUS);
        }

        [HttpPost]
        public ActionResult ComfirmResult(AboutUsModel model)
        {
            string FilePath = Server.MapPath("~/Json/");
            string fileName = "aboutus.json";
            var str = System.IO.File.ReadAllText(FilePath + fileName);

            AboutUsViewModel infoList = new JavaScriptSerializer().Deserialize<AboutUsViewModel>(str);

            infoList.aboutUs[(int)Session["AboutUsId"]] = model;

            var jsondata = new JavaScriptSerializer().Serialize(infoList);

            System.IO.File.WriteAllText(FilePath + fileName, jsondata);

            return RedirectToAction("Index", "Movies", null);
        }

        [HttpGet]
        public ActionResult AddMediaFromImdb()
        {
            return View("AddMediaFromIMDB");
        }

        [HttpPost]
        public async Task<ActionResult> AddMediaFromImdbById(AddMediaView model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddMediaFromIMDB");
            }

            ISeed dbSeed = new DBSeed();

            await dbSeed.AddMovieByImbdId(model.ImdbId);

            return RedirectToAction("Index", "Movies", null);
        }
    }
}
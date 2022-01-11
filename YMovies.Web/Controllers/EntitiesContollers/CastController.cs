using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
using YMovies.Web.Utilities;
using YMovies.Web.ViewModels;

namespace YMovies.Web.Controllers
{
    public class CastController : Controller
    {
        public CastController(IService<CastDto> castService)
        {
            _castService = castService;
        }

        private IService<CastDto> _castService;

        [HttpGet]
        public string Casts(string query = null)
        {
            var temp = _castService.Items.ToList();

            if (!string.IsNullOrWhiteSpace(query))
                temp = temp.Where(t => t.Name.Contains(query)).ToList();

            var listOfCasts = AutoMap.Mapper.Map<List<CastDto>, List<CastViewModel>>(temp);

            var json = JsonConvert.SerializeObject(
                listOfCasts,
                Formatting.Indented,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            return json;
        }

        public async Task<ActionResult> Index()
        {
            return View(_castService.Items.ToList());
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var castDto = _castService.GetItem(id.Value);
            if (castDto == null)
            {
                return HttpNotFound();
            }
            return View(castDto);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Surname,PictureUrl")] CastDto castDto)
        {
            if (ModelState.IsValid)
            {
                _castService.AddItem(castDto);
                return RedirectToAction("Index");
            }

            return View(castDto);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastDto castDto = _castService.GetItem(id.Value);
            if (castDto == null)
            {
                return HttpNotFound();
            }
            return View(castDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Surname,PictureUrl")] CastDto castDto)
        {
            if (ModelState.IsValid)
            {
                _castService.UpdateItem(castDto);
                return RedirectToAction("Index");
            }
            return View(castDto);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CastDto castDto = _castService.GetItem(id.Value);
            if (castDto == null)
            {
                return HttpNotFound();
            }
            return View(castDto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var dto = _castService.GetItem(id);
            _castService.DeleteItem(dto);
            return RedirectToAction("Index");
        }

    }
}

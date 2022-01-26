using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;
using YMovies.Web.Utilites;

namespace YMovies.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CastController : Controller
    {
        public CastController(IService<CastDto> castService)
        {
            _castService = castService;
        }

        private readonly IService<CastDto> _castService;

        [HttpGet]
        public string Casts(string query = null)
        {
            var actors = _castService.Items;

            if (!string.IsNullOrWhiteSpace(query))
                actors = actors.Where(t => t.Name.Contains(query)).ToList();

            return TypeConverter.ToJson(actors);
        }

        public async Task<ActionResult> Index(int page = 1)
        { 
            return View(_castService.Items);
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
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] CastDto castDto)
        {
            if (!string.IsNullOrWhiteSpace(castDto.Name))
            {
                _castService.AddItem(castDto);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Name", "Name can't be empty");
            return View(castDto);
        }

        public async Task<ActionResult> Edit(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] CastDto castDto)
        {
            if (!string.IsNullOrWhiteSpace(castDto.Name))
            {
                _castService.UpdateItem(castDto);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("Name", "Name can't be empty");
            return View(castDto);
        }

        public async Task<ActionResult> Delete(int? id)
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

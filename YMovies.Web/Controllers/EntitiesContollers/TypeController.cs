﻿using System.Web.Mvc;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.IService;

namespace YMovies.Web.Controllers.EntitiesContollers
{
    [Authorize(Roles = "admin")]
    public class TypeController : Controller
    {
        public TypeController(IService<TypeDto> genreService)
        {
            _typeService = genreService;
        }

        private IService<TypeDto> _typeService;

        [HttpGet]
        public ActionResult Index() => View(_typeService.Items);

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(TypeDto model)
        {
            if (ModelState.IsValid)
            {
                _typeService.AddItem(model);
                RedirectToAction("Index", "Genre");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var genre = _typeService.GetItem(id.Value);
            if (genre == null)
                return HttpNotFound();
            return View(genre);
        }

        [HttpPost]
        public ActionResult Edit(TypeDto model)
        {
            if (ModelState.IsValid)
            {
                _typeService.UpdateItem(model);
                return RedirectToAction("Index", "Genre");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var genre = _typeService.GetItem(id.Value);
            if (genre == null)
                return HttpNotFound();
            return View(genre);
        }

        [HttpPost]
        public ActionResult Delete(TypeDto model)
        {
            _typeService.DeleteItem(model);
            return RedirectToAction("Index", "Genre");
        }
    }
}
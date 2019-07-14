using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPortalı.Areas.Amdin.Controllers
{
    [Authorize(Roles = "A")]
    public class FilmController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();
        // GET: Amdin/Film
        public ActionResult Index()
        {
            List<Films> films = db.Films.OrderBy(f => f.FCDate).ToList();
            return View(films);
        }

        public ActionResult AddFilm()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFilm(Films film) {

            if (ModelState.IsValid)
            {
                film.FCDate = DateTime.Now;
                db.Films.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        public ActionResult DeleteFilm(int filmId)
        {

            var inFilms = db.Films.FirstOrDefault(f => f.FId == filmId);

            if(inFilms != null)
            {
                db.Films.Remove(inFilms);
                db.SaveChanges();
                return Content("Başarılı");
            }
            return Content("Film Bulunamadı");
        }
    }
}
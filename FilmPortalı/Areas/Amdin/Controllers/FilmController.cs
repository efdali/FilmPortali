using FilmPortalı.Areas.Amdin.ViewModel;
using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPortalı.Areas.Amdin.Controllers
{
    //[Authorize(Roles = "A")]
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
            AddFilmViewModel vm = new AddFilmViewModel
            {
                category = db.Categories.ToList()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFilm(AddFilmViewModel vm)
        {
            if (vm.film.FId == 0)
            {
                if (ModelState.IsValid)
                {
                    List<int> qwe = new List<int>();
                    foreach (var cat in vm.category)
                    {
                        qwe.Add(cat.CId);
                    }
                    for (int i = 0; i < qwe.Count(); i++)
                        db.spFilmEkle(vm.film.FName, vm.film.FSummary, vm.film.FYear, vm.film.FCountry, vm.film.FImdb,
                            vm.film.FPoster, vm.film.FTrailer, vm.film.FSeo, qwe[i]);

                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
        }
            return View(vm);
        }

        public ActionResult UpdateFilm(int filmId)
        {
            AddFilmViewModel vm = new AddFilmViewModel
            {
                category = db.Categories.ToList()
            };
            var film = db.Films.Find(filmId);
            if (film == null)
                return HttpNotFound();

            vm.film = film;

            return View("AddFilm", vm);
        }

        public ActionResult DeleteFilm(int filmId)
        {
            db.spFilmSil(filmId);
            return Content("Film Silindi");
        }
    }
}
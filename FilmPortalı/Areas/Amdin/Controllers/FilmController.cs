using FilmPortalı.Areas.Amdin.ViewModel;
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
        FilmPortaliEntities1 db = new FilmPortaliEntities1();
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
            if (ModelState.IsValid)
            {
                List<string> qwe = new List<string>();
                for (int i = 0; i < vm.category.Count(); i++)
                {
                    if (vm.category[i].CStatus == true)
                        qwe.Add(vm.category[i].CAd);
                }
                for (int i = 0; i < qwe.Count(); i++)
                    db.spFilmEkle(vm.film.FName, vm.film.FSummary, vm.film.FYear, vm.film.FCountry, vm.film.FImdb,
                        vm.film.FPoster, vm.film.FTrailer, vm.film.FSeo, qwe[i]);

                db.SaveChanges();
                return RedirectToAction("Index");
            }  
            return View(vm.film);
        }

        public ActionResult DeleteFilm(int filmId)
        {

            var inFilms = db.Films.FirstOrDefault(f => f.FId == filmId);

            if (inFilms != null)
            {
                db.Films.Remove(inFilms);
                db.SaveChanges();
                return Content("Başarılı");
            }
            return Content("Film Bulunamadı");
        }
    }
}
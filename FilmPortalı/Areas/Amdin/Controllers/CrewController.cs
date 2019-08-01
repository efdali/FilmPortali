using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FilmPortalı.Models;

namespace FilmPortalı.Areas.Amdin.Controllers
{
    [Authorize(Roles = "A")]
    public class CrewController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();
        // GET: Amdin/Crew
        public ActionResult Index()
        {
            var crews = db.Crews.ToList();
            return View(crews);
        }

        public ActionResult DeleteCrew(int id)
        {
            var crew = db.Crews.Find(id);
            if (crew == null) return HttpNotFound();

            var filmCrewList = db.FilmCrew.Where(fc => fc.CId == id).ToList();
            db.Crews.Remove(crew);
            db.FilmCrew.RemoveRange(filmCrewList);
            db.SaveChanges();

            return Json(new { text = "Silindi" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddCrew()
        {
            return View(new Crews());
        }

        public ActionResult UpdateCrew(int id)
        {
            var crew = db.Crews.Find(id);
            if (id == null) return HttpNotFound();
            return View("AddCrew", crew);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCrew(Crews crew)
        {
            if (!ModelState.IsValid)
            {
                return View("AddCrew", crew);
            }
            if (crew.CId != 0) db.Entry(crew).State = EntityState.Modified;
            else db.Crews.Add(crew);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FilmCrew()
        {
            var filmCrew = (from fc in db.FilmCrew
                            join c in db.Crews on fc.CId equals c.CId
                            join f in db.Films on fc.FId equals f.FId
                            select fc).ToList();
            return View(filmCrew);
        }

        public ActionResult AddCrewToFilm()
        {
            ViewBag.Films = db.Films.ToList();
            ViewBag.Crews = db.Crews.ToList();

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCrewToFilm(FilmCrew filmCrew)
        {

            var inFc = db.FilmCrew.Where(fc => fc.FId == filmCrew.FId && fc.CId == filmCrew.CId).FirstOrDefault();
            if (inFc != null) return RedirectToAction("FilmCrew");

            db.FilmCrew.Add(filmCrew);
            db.SaveChanges();


            return RedirectToAction("FilmCrew");
        }

        public ActionResult DeleteFilmCrew(int id)
        {

            var filmCrew = db.FilmCrew.Find(id);
            if (filmCrew == null) return Json(new { text = "Kayıt Bulunamadı" }, JsonRequestBehavior.AllowGet);


            db.FilmCrew.Remove(filmCrew);
            db.SaveChanges();
            return Json(new { text = "Silindi" }, JsonRequestBehavior.AllowGet);
        }
    }
}
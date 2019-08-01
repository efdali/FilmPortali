using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmPortalı.Models;

namespace FilmPortalı.Areas.Amdin.Controllers
{
    [Authorize(Roles = "A")]
    public class SourceController : Controller
    {
        FilmPortaliEntities db =new FilmPortaliEntities();
        // GET: Amdin/Source
        public ActionResult Index()
        {
            var sources = (from s in db.Sources
                join f in db.Films on s.SFId equals f.FId where s.SStatus==true
                select s).ToList();

            return View(sources);
        }

        public ActionResult AddSource()
        {
            ViewBag.Films = db.Films.ToList();
            return View();
        }

        public ActionResult UpdateSource(int id)
        {
            var source = db.Sources.Find(id);
            if (source == null) return HttpNotFound();

            ViewBag.Films = db.Films.ToList();
            return View("AddSource", source);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddorEdit(Sources source)
        {
            if (source.SId == 0) db.Sources.Add(source);
            else db.Entry(source).State = EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteSource(int id)
        {
            var source = db.Sources.Find(id);
            source.SStatus = false;
            db.Entry(source).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new {statu = 1});
        }
    }
}
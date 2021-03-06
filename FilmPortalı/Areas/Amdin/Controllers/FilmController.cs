﻿using FilmPortalı.Areas.Amdin.ViewModel;
using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            AddFilmViewModel vm = new AddFilmViewModel();
            vm.category = db.Categories.ToList();
            vm.film=new Films();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFilm(Films film, List<int> cats,Sources source,bool slider)
        {
            if (cats == null) cats = new List<int>();

            if (cats.Count != 0)
            {
                for (int i = 0; i < cats.Count(); i++)
                    db.spFilmEkle1(film.FId,film.FName, film.FSummary, film.FYear, film.FCountry, film.FImdb,
                        film.FPoster, film.FTrailer, film.FSeo, film.FTurkishName, cats[i],source.SName,source.SUrl,film.FKeywords,film.FDescription,slider);
            }
            else
            {
                db.spFilmEkle1(film.FId,film.FName, film.FSummary, film.FYear, film.FCountry, film.FImdb,
                        film.FPoster, film.FTrailer, film.FSeo, film.FTurkishName, 0,source.SName,source.SUrl,film.FKeywords,film.FDescription,slider);
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult GetFilmCategories(int filmId)
        {

            var cats = (from c in db.Categories
                        join fc in db.FilmCategory.Where(fc => fc.FId == filmId) on c.CId equals fc.CId
                        select new { c.CId, c.CAd }).ToList();

            return Json(new { list = cats }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult DeleteCats(int filmId, int catId)
        {
            var cats = db.FilmCategory.FirstOrDefault(fc => fc.FId == filmId && fc.CId == catId);
            if (cats != null)
            {
                db.FilmCategory.Remove(cats);
                db.SaveChanges();
                return Content("Başarılı");
            }
            return Content("Başarısız");
        }

        public ActionResult AddToSlider(int filmId)
        {
            var inSlider = db.Slider.Where(s => s.SFId == filmId).FirstOrDefault();
            if (inSlider != null)
            {
                if (inSlider.SStatus == false)
                {
                    inSlider.SStatus = true;
                    db.Entry(inSlider).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new {text = "Aktif Edildi"});
                }
                return Json(new { text = "Zaten Mevcut"});
            }
            Slider slider = new Slider();
            slider.SStatus = true;
            slider.SFId = filmId;
            slider.SDate=DateTime.Now;
            db.Slider.Add(slider);
            db.SaveChanges();

            return Json(new {text = "Slidera Eklendi"});
        }

        public ActionResult UpdateFilm(int filmId)
        {
            AddFilmViewModel vm = new AddFilmViewModel();
            vm.category = db.Categories.ToList();
            vm.source = db.Sources.FirstOrDefault(s => s.SFId == filmId);
            var film = db.Films.Find(filmId);
            if (film == null)
                return HttpNotFound();

            vm.film = film;

            return View("AddFilm", vm);
        }

        public ActionResult DeleteFilm(int filmId)
        {
            db.spFilmSil1(filmId);
            return Content("Film Silindi");
        }
    }
}
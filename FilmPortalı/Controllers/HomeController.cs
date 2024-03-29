﻿using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPortalı.Controllers
{
    public class HomeController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();
        // GET: Home
        public ActionResult Index()
        {
            List<Slider> slider = db.Slider.Include("Films").OrderByDescending(s => s.SId).Where(s => s.SStatus == true).Take(10).ToList();
            ViewBag.Count = db.Films.Count();
            return View(slider);
        }
        [Route("Home/GetFilms/{page?}")]
        public ActionResult GetFilms(int page = 0)
        {
            List<Films> films = db.Films.OrderByDescending(f => f.FId).Skip(page * 40).Take(40).ToList();
            return View(films);
        }

        [Route("tur/{tur}")]
        public ActionResult Tur(string tur)
        {
            string sort = Request.QueryString["sortBy"] == null ? "imdb" : Request.QueryString["sortBy"];

            if (tur == "2019")
            {
                ViewBag.Header = "2019 Yılı Filmleri";
            }
            else if (tur == "en-cok-izlenenler")
            {

                ViewBag.Header = "En Çok İzlenen Filmler";
            }
            else
            {
                ViewBag.Header = tur + " kategorisindeki filmler";
            }
            ViewBag.active = sort;
            ViewBag.tur = tur;
            ViewBag.Count = db.Films.Count();
            return View("Tur");
        }

        public ActionResult GetType(string tur, string sort, int page = 0)
        {
            List<Films> films = null;
            if (tur == "2019")
            {
                if (sort == "izlenme")
                {
                    films = db.Films.Where(f => f.FYear.ToString() == "2019")
                    .OrderByDescending(f => f.FUDate).Skip(page * 40).Take(40).ToList();
                    //films = (from f in db.Films
                    //         join v in db.Views.GroupBy(v => v.VFId).
                    //         Select(v => new { sayi = v.Count(), VFId = v.VFId }) on f.FId equals v.VFId).OrderByDescending(v.sayi).ToList();

                }
                else if (sort == "eklenme")
                {
                    films = db.Films.Where(f => f.FYear.ToString() == "2019")
                    .OrderByDescending(f => f.FCDate).Skip(page * 40).Take(40).ToList();
                }
                else
                {
                    films = db.Films.Where(f => f.FYear.ToString() == "2019")
                        .OrderByDescending(f => f.FImdb).Skip(page * 40).Take(40).ToList();
                }
            }
            else if (tur == "en-cok-izlenenler")
            {
                if (sort == "izlenme")
                {
                    films = db.Films.OrderByDescending(f => f.FUDate).Skip(page * 40).Take(40).ToList();
                }
                else if (sort == "eklenme")
                {
                    films = db.Films.OrderByDescending(f => f.FCDate).Skip(page * 40).Take(40).ToList();
                }
                else
                {
                    films = db.Films.OrderByDescending(f => f.FImdb).Skip(page * 40).Take(40).ToList();
                }
            }
            else
            {
                if (sort == "izlenme")
                {
                    films = (from f in db.Films
                             join fc in db.FilmCategory on f.FId equals fc.FId
                             join c in db.Categories.Where(c => c.CAd.Contains(tur)) on fc.CId equals c.CId
                             select f).OrderByDescending(f => f.FUDate).Skip(page * 40).Take(40).ToList();

                }
                else if (sort == "eklenme")
                {
                    films = (from f in db.Films
                             join fc in db.FilmCategory on f.FId equals fc.FId
                             join c in db.Categories.Where(c => c.CAd.Contains(tur)) on fc.CId equals c.CId
                             select f).OrderByDescending(f => f.FCDate).Skip(page * 40).Take(40).ToList();

                }
                else
                {
                    films = (from f in db.Films
                             join fc in db.FilmCategory on f.FId equals fc.FId
                             join c in db.Categories.Where(c => c.CAd.Contains(tur)) on fc.CId equals c.CId
                             select f).OrderByDescending(f => f.FImdb).Skip(page * 40).Take(40).ToList();

                }

            }
            return View("GetFilms", films);
        }

        [Route("rastgele")]
        public ActionResult GetRandomFilm()
        {
            int minId = db.Films.Min(f => f.FId);
            int maxId = db.Films.Max(f => f.FId);
            Random random = new Random();
            int rast = random.Next(minId, maxId);

            string filmName = null;

            while (filmName == null)
            {
                rast = random.Next(minId, maxId);
                filmName = (from f in db.Films
                            where f.FId == rast
                            select f.FSeo).FirstOrDefault();
            }

            return Json(new {filmName},JsonRequestBehavior.AllowGet);
        }
    }

    //// GET 2019
    //[Route("2019")]
    //public ActionResult Year()
    //{
    //    string sort = Request.QueryString["sortBy"] == null ? "imdb" : Request.QueryString["sortBy"];
    //    List<Films> films = null;
    //    if (sort == "izlenme")
    //    {
    //        films = db.Films.Where(f => f.FYear.Value.Year == 2019)
    //            .OrderByDescending(f => f.FUDate).Skip(0).Take(40).ToList();
    //    }
    //    else if (sort == "eklenme")
    //    {
    //        films = db.Films.Where(f => f.FYear.Value.Year == 2019)
    //        .OrderByDescending(f => f.FCDate).Skip(0).Take(40).ToList();
    //    }
    //    else
    //    {
    //        films = db.Films.Where(f => f.FYear.Value.Year == 2019)
    //            .OrderByDescending(f => f.FImdb).Skip(0).Take(40).ToList();
    //    }
    //    ViewBag.active = sort;
    //    ViewBag.Header = "2019 Yılı Filmleri";
    //    return View("Tur", films);
    //}

    //// GET En çok izlenen filmler
    //[Route("en-cok-izlenenler")]
    //public ActionResult MostWatchedFilms()
    //{
    //    string sort = Request.QueryString["sortBy"] == null ? "imdb" : Request.QueryString["sortBy"];
    //    List<Films> films = null;
    //    if (sort == "izlenme")
    //    {
    //        films = db.Films.OrderByDescending(f => f.FUDate).Skip(0).Take(40).ToList();
    //    }
    //    else if (sort == "eklenme")
    //    {
    //        films = db.Films.OrderByDescending(f => f.FCDate).Skip(0).Take(40).ToList();
    //    }
    //    else
    //    {
    //        films = db.Films.OrderByDescending(f => f.FImdb).Skip(0).Take(40).ToList();
    //    }
    //    ViewBag.active = sort;
    //    ViewBag.Header = "En Çok İzlenen Filmler";
    //    return View("Tur", films);
    //}

}
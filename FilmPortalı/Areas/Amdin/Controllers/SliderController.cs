using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmPortalı.Models;
using Microsoft.Ajax.Utilities;

namespace FilmPortalı.Areas.Amdin.Controllers
{
    [Authorize(Roles = "A")]
    public class SliderController : Controller
    {
        FilmPortaliEntities db=new FilmPortaliEntities();
        // GET: Amdin/Slider
        public ActionResult Index()
        {

            var sliders = db.Slider.OrderBy(s => s.SDate).ToList();
            return View(sliders);
        }

        public ActionResult AddSlider()
        {

            ViewBag.Films = (from f in db.Films select new {f.FId, f.FName, f.FYear}).ToList();

            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSlider(Slider slider)
        {

            slider.SDate=DateTime.Now;
            db.Slider.Add(slider);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult showOrHideSlider(int id)
        {
            var slider = db.Slider.Find(id);
            if (slider == null) return HttpNotFound();

            if (slider.SStatus == true) slider.SStatus = false;
            else slider.SStatus = true;
            
            db.Entry(slider).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new {text = "Slider Görünürlüğü Değiştirildi"},JsonRequestBehavior.AllowGet);
        }

        public ActionResult deleteSlider(int id)
        {
            var slider = db.Slider.Find(id);

            if (slider == null) return HttpNotFound();

            db.Slider.Remove(slider);
            db.SaveChanges();
            return Json(new {text = "Slider Silindi"},JsonRequestBehavior.AllowGet);
        }
    }
}
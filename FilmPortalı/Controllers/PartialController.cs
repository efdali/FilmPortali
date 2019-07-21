using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPortalı.Controllers
{
    public class PartialController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();
        // GET: Partial
        public ActionResult Index()
        {
            return Content("");
        }
        public ActionResult Header()
        {
            List<Categories> categories = db.Categories.ToList();
            return PartialView(categories);
        }

        public ActionResult Footer()
        {
            return PartialView();
        }

        public ActionResult SearchFilm(string val)
        {
            List<Films> films = db.Films.Where(f => f.FName.ToLower().Contains(val) || f.FTurkishName.Contains(val)).Take(5).ToList();
            return View(films);
        }
    }
}
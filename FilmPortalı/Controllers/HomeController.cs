using FilmPortalı.Models;
using FilmPortalı.ViewModel;
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
            AnasayfaSliderFilmViewModel vm = new AnasayfaSliderFilmViewModel();
            List<Slider> slider = db.Slider.Include("Films").Where(s => s.SStatus == true).Take(10).ToList();
            List<Films> film = db.Films.OrderBy(f => f.FId).Skip(0).Take(40).ToList();
            vm.slider = slider;
            vm.film = film;
            return View(vm);
        }

        public ActionResult Tur(int id)
        {
            List<Films> films = db.Films.Where(f => f.FYear.Value.Year == id).ToList();
            return View(films);
        }

    }
}
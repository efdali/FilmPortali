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
            List<Slider> slider = db.Slider.ToList();
            List<Films> film = db.Films.ToList();
            vm.slider = slider;
            vm.film = film;
            return View(vm);
        }

        public ActionResult Tur()
        {
            return View();
        }
    }
}
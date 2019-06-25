using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPortalı.Controllers
{
    public class FilmController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();
        // GET Film Detay
        [Route("detay/{filmName}")]
        public ActionResult FilmDetails(string filmName)
        {
            Films film = db.Films.Include("FilmCategory")
                .Where(f => f.FName == filmName).FirstOrDefault();
            return View(film);
        }
    }
}
using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPortalı.Controllers
{
    public class ProfileController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();
        // GET: Profile
        public ActionResult Index()
        {
            Users user = db.Users.Where(u => u.UId == (int)Session["kullaniciId"]).FirstOrDefault();
            return View(user);
        }
    }
}
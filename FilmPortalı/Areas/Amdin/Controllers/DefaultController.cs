using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FilmPortalı.Models;

namespace FilmPortalı.Areas.Amdin.Controllers
{
    [Authorize(Roles = "A")]
    public class DefaultController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();
        // GET: Amdin/Home
        public ActionResult Index()
        {

            ViewBag.TotalUser = db.Users.Count();
            ViewBag.TotalFilm = db.Films.Count();
            ViewBag.LastLogin = db.Users.OrderByDescending(u => u.UDate).FirstOrDefault().UNick;
            ViewBag.LastSession = db.Users.OrderByDescending(u => u.ULastSession).FirstOrDefault().UNick;
            ViewBag.LastComment = db.Comments.Count() >0 ? db.Comments.OrderByDescending(c => c.CDate).FirstOrDefault().CText : "";
            ViewBag.LastWatch = db.Views.Include("Films").OrderByDescending(v => v.VDate).FirstOrDefault().Films.FName;
            return View();
        }

        public ActionResult GetUserInfo()
        {
            var ist = db.Users.GroupBy(u => u.UDate).OrderBy(u => u.Key).ToArray()
                .Select(u => new { Date = string.Format("{0:dd-MM-yyyy}", u.Key), Count = u.Count() }).ToList();

            return Json(ist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTotalFilmInfo()
        {

            var ist = db.Views.Include("Films").GroupBy(v => v.VFId).OrderByDescending(v => v.Count())
                .Select(v => new { Film = v.Select(v1 => v1.Films.FName).FirstOrDefault(), Count = v.Count() }).Take(10).ToList();

            return Json(ist, JsonRequestBehavior.AllowGet);
        }
    }
}
using FilmPortalı.Models;
using FilmPortalı.ViewModel;
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

            FilmDetailsViewModel vm = new FilmDetailsViewModel();
            Films film = db.Films
                .Where(f => f.FSeo == filmName).FirstOrDefault();
            List<Comments> cmts = db.Comments.Include("SubComments").Where(c => c.CFId == film.FId && c.CStatus == true).OrderByDescending(c => c.CDate).ToList();
            List<Sources> src = db.Sources.Where(s => s.FId == film.FId).ToList();
            List<FilmCrew> crew = db.FilmCrew.Include("Crews").Where(c => c.FId == film.FId && c.FCMission == "Oyuncu").ToList();
            FilmCrew director = db.FilmCrew.Include("Crews").Where(fc => fc.FId == film.FId && fc.FCMission == "Yönetmen").FirstOrDefault();
            List<Categories> categories = (from c in db.Categories
                                           join fc in db.FilmCategory.Where(fc => fc.FId == film.FId) on c.CId equals fc.CId
                                           select c).ToList();
            string category="";
            int p = 0;
            foreach(Categories cat in categories)
            {
                category += cat.CAd;
                if (p != categories.Count - 1)
                {
                    category += ",";
                }
                p++;
            }
            vm.film = film;
            vm.comment = cmts;
            vm.filmSource = src;
            vm.crew = crew;
            ViewBag.Director = director!=null ? director.Crews.CName.ToString() : "-";
            ViewBag.Categories=category;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddComment(string filmId, string commentText)
        {

            Comments comments = new Comments();
            comments.CText = commentText;
            comments.CFId = Convert.ToInt16(filmId);
            comments.CUId = Convert.ToInt16(Session["kullaniciId"]);
            comments.CDate = DateTime.Now;
            comments.CStatus = true;
            db.Comments.Add(comments);
            db.SaveChanges();

            return Content("<div class='yorum-container'>" +
                "<div class='user'>" +
                "<img src=" + Session["kullaniciResim"] + "  alt=" + User.Identity.Name + " class='user-image'>" +
                "<span>" + User.Identity.Name + "</span>" +
                "</div><div class='yorum'>" +
                "<span class='blur-text'><i class='far fa-clock'></i>Az Önce</span>" +
                "<p style='margin:0'>" + comments.CText + "</p>" +
                "</div>" +
                "</div>");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddSubComment(string commentId, string commentText)
        {

            SubComments comment = new SubComments();
            comment.SCText = commentText;
            comment.SCCId = Convert.ToInt16(commentId);
            comment.SCUId = Convert.ToInt16(Session["kullaniciId"]);
            comment.SCDate = DateTime.Now;
            comment.SCStatus = true;
            db.SubComments.Add(comment);
            db.SaveChanges();

            return Content("Başarılı");
        }

        public ActionResult AddLikes(int filmId, int type)
        {

            int userId = Convert.ToInt16(Session["kullaniciId"]);
            List list = null;
            list = db.List.Where(l => l.LUId == userId && l.LFId == filmId && l.LType != 0).FirstOrDefault();
            if (list != null && list.LType != 0)
            {
                if (list.LType == -type)
                {
                    list.LType = type;
                }
            }
            else if (list == null)
            {
                list = new List();
                list.LFId = filmId;
                list.LUId = userId;
                list.LType = type;
                db.List.Add(list);
            }
            db.SaveChanges();
            return Content("<i class='far fa-check-circle'></i> Listenizde");
        }

        public ActionResult AddList(int filmId)
        {

            int userId = Convert.ToInt16(Session["kullaniciId"]);
            List list = null;
            list = db.List.Where(l => l.LUId == userId && l.LFId == filmId && l.LType == 0).FirstOrDefault();
            if (list == null)
            {
                list = new List();
                list.LFId = filmId;
                list.LUId = userId;
                list.LType = 0;
                db.List.Add(list);
            }
            db.SaveChanges();
            return Content("<i class='far fa-check-circle'></i> Listenizde");
        }

        public JsonResult CheckList(int filmId)
        {
            int userId = Convert.ToInt16(Session["kullaniciId"]);
            var like = db.List.Where(l => l.LFId == filmId && l.LUId == userId && l.LType != 0).FirstOrDefault();
            var list = db.List.Where(l => l.LFId == filmId && l.LUId == userId && l.LType == 0).FirstOrDefault();
            return Json(new { InList = list==null ? 0 : 1,InLike = like == null ? 0 : like.LType },JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddWatched(int filmId) {

            string userId = Session["kullaniciId"] == null ? "1" : Session["kullaniciId"].ToString();
            Views v = new Views();
            v.VFId = filmId;
            v.VUId = Convert.ToInt16(userId);
            db.Views.Add(v);
            db.SaveChanges();
            return Content("Başarılı");
        }

    }

}


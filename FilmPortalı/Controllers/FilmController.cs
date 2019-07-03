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
            Films film =db.Films
                .Where(f => f.FName == filmName).FirstOrDefault();
            List<Comments> cmts = db.Comments.Include("SubComments").Where(c => c.CFId == film.FId && c.CStatus==true).ToList();
            List<FilmSource> src = db.FilmSource.Where(s => s.FId == film.FId).ToList();
            List<FilmCrew> crew = db.FilmCrew.Include("Crews").Where(c => c.FId == film.FId && c.FCMission=="Oyuncu").ToList();
            vm.film = film;
            vm.comment = cmts;
            vm.filmSource = src;
            vm.crew = crew;
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddComment(string filmId,string commentText)
        {

            Comments comments = new Comments();
            comments.CText = commentText;
            comments.CFId = Convert.ToInt16(filmId);
            comments.CUId = Convert.ToInt16(Session["kullaniciId"]);
            comments.CDate = DateTime.Now;
            comments.CStatus = true;
            db.Comments.Add(comments);
            db.SaveChanges();

            return Content("Başarılı");
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
    }

}
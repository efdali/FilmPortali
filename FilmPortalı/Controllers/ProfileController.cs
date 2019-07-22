using FilmPortalı.Models;
using FilmPortalı.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FilmPortalı.Controllers
{
    [Authorize()]
    public class ProfileController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();
        // GET: Profile
        [Route("profil")]
        public ActionResult Index()
        {
            int userId = Convert.ToInt16(User.Identity.Name);
            Users user = db.Users.Where(u => u.UId == userId).FirstOrDefault();
            return View(user);
        }

        [HttpGet]
        public ActionResult GetList()
        {
            int userId = Convert.ToInt16(User.Identity.Name);
            List<List> watch = db.List.Include("Films").Where(l => l.LUId == userId && l.LType == 0).ToList();
            List<List> likes = db.List.Include("Films").Where(l => l.LUId == userId && l.LType == 1).ToList();
            List<List> dislikes = db.List.Include("Films").Where(l => l.LUId == userId && l.LType == -1).ToList();
            ProfileListViewModel vm = new ProfileListViewModel();
            vm.watch = watch;
            vm.likes = likes;
            vm.dislikes = dislikes;
            return View(vm);
        }

        [HttpGet]
        public ActionResult GetComment()
        {
            int userId = Convert.ToInt16(User.Identity.Name);
            List<Comments> comments = db.Comments.Include("Films").Where(c => c.CUId == userId && c.CStatus == true).ToList();
            return View(comments);
        }

        [HttpPost]
        public ActionResult UpdateInfo(Users user, string gender)
        {
            var guncellenecekUser = db.Users.Find(user.UId);
            if (guncellenecekUser == null)
            {
                return HttpNotFound();
            }

            guncellenecekUser.UName = user.UName;
            guncellenecekUser.USurname = user.USurname;
            if (user.UBirthDate != null)
                guncellenecekUser.UBirthDate = user.UBirthDate;
            guncellenecekUser.UGender = gender == "0" ? false : true;
            guncellenecekUser.UTwitter = user.UTwitter;
            guncellenecekUser.UInstagram = user.UInstagram;
            guncellenecekUser.UInfo = user.UInfo;

            db.Entry(guncellenecekUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Content("Güncelleme Başarılı");
        }

        [HttpPost]
        public ActionResult UpdateAccount(Users user, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", user);
            }
            var guncellenecekUser = db.Users.Find(user.UId);
            if (guncellenecekUser == null)
            {
                return HttpNotFound();
            }
            if (user.UPasswd != null)
            {
                user.UPasswd = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UPasswd, "MD5");
                guncellenecekUser.UPasswd = user.UPasswd;
            }

            guncellenecekUser.UNews = user.UNews;
            guncellenecekUser.UAppear = user.UAppear;

            if (image != null)
            {
                string rsm = guncellenecekUser.UNick;
                rsm += image.FileName;
                image.SaveAs(Server.MapPath("/Public/img/" + rsm));
                if (guncellenecekUser.UImage != null && guncellenecekUser.UImage.Contains("/Public/img/"))
                    System.IO.File.Delete(Server.MapPath(guncellenecekUser.UImage));

                guncellenecekUser.UImage = "/Public/img/" + rsm;
                Session["kullaniciResim"] = "/Public/img/" + rsm;
            }
            else if (user.UImage != null && !user.UImage.Equals(guncellenecekUser.UImage))
            {
                if (guncellenecekUser.UImage.Contains("/Public/img"))
                    System.IO.File.Delete(Server.MapPath(guncellenecekUser.UImage));
                guncellenecekUser.UImage = user.UImage;
                Session["kullaniciResim"] = user.UImage;
            }

            db.Entry(guncellenecekUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Content("Güncelleme Başarılı");
        }

        public ActionResult DeleteComment(int id)
        {
            Comments comment = db.Comments.Find(id);
            if (comment == null)
            {
                return Content("Yorum Bulunamadı.");
            }
            comment.CStatus = false;
            db.Entry(comment).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Content("Başarılı");
        }

        public ActionResult RemoveList(int filmId, int type)
        {
            var inList = db.List.Where(l => l.LFId == filmId && l.LType == type).FirstOrDefault();

            if (inList != null)
            {
                db.List.Remove(inList);
                db.SaveChanges();
            }

            return Content("Listeden Silindi.");
        }
    }
}
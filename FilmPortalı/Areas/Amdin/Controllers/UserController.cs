using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace FilmPortalı.Areas.Amdin.Controllers
{
    [Authorize(Roles = "A")]
    public class UserController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();
        // GET: Amdin/User
        public ActionResult Index()
        {
            var users = db.Users.Where(u => u.URole == "U").ToList();
            return View(users);
        }

        public ActionResult SuspendedUser()
        {
            var users = db.Users.Where(u => u.URole == "U" && u.UStatus == false).ToList();
            return View("Index", users);
        }

        // TODO user details
        public ActionResult UserDetails(int userId)
        {
            return Content("Detaylar");
        }

        public ActionResult RemoveSuspend(int userId)
        {
            var user = db.Users.Find(userId);
            if(user == null)
            {
                return Content("Kullanıcı Bulunamadı.");
            }

            user.UStatus = true;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Content("Hesap aktif edildi.");

        }

        public ActionResult SuspendUser(int userId)
        {
            var user = db.Users.Find(userId);
            if(user == null)
            {
                return Content("Kullanıcı Bulunamadı.");
            }

            user.UStatus = false;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return Content("Askıya Alındı.");
        }

        public ActionResult GiveAdminPermission(int userId)
        {
            var user = db.Users.Find(userId);
            if(user != null)
            {
                user.URole = "A";
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return Content("Adminlik Yetkisi Verildi.");
            }

            return Content("Bir Hata Oluştu.Lütfen Daha Sonra Tekrar Deneyin.");
        }
    }
}
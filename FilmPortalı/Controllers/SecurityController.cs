using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FilmPortalı.Controllers
{
    public class SecurityController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index#giris-modal","Home");
            }

            return Content("Kayıt Başarısız");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Sign(Users user)
        {
            var userIn = db.Users.FirstOrDefault(u => user.UNick == u.UNick && user.UPasswd == u.UPasswd);
            string url = Request.UrlReferrer.ToString();
            if (userIn != null)
            {
                Session["kullaniciId"] = userIn.UId;
                Session["kullaniciResim"] = userIn.UImage;
                FormsAuthentication.SetAuthCookie(user.UNick, false);
                return Redirect(url);
            }

            return Content("Giriş Başarısız");
        }

        public ActionResult LogOut()
        {
            string comingUrl = Request.UrlReferrer.ToString();
            FormsAuthentication.SignOut();
            Session.Remove("kullaniciId");
            Session.Remove("kullaniciResim");
            return Redirect(comingUrl);
        }
    }
}
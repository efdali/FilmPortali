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

        public ActionResult Index()
        {
            return Content("Deneme");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return View("Mesaj") ;
            }

            return View("Mesaj");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Sign(Users user)
        {
            var userIn = db.Users.FirstOrDefault(u=>user.UNick==u.UNick&&user.UPasswd==u.UPasswd);

            if (userIn != null)
            {
                Session["kullaniciId"] = userIn.UId;
                Session["kullaniciResim"] = userIn.UImage;
                FormsAuthentication.SetAuthCookie(user.UNick, false);
                return RedirectToAction("Index","Home");
            }

            return Content("Mesaj");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Remove("kullaniciId");
            Session.Remove("kullaniciResim");
            return RedirectToAction("Index","Home");
        }
    }
}
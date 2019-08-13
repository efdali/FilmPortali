using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace FilmPortalı.Controllers
{
    public class SecurityController : Controller
    {
        FilmPortaliEntities db = new FilmPortaliEntities();

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Login(Users users)
        {
            if (ModelState.IsValid)
            {

                Users user = db.Users.Where(u => u.UNick == users.UNick || u.UMail == users.UMail).FirstOrDefault();
                if(user != null)
                {
                    return Json(new { status = 0, message = "Nick veya Mail adresi kullanımda." });
                }
                users.URole = "U";
                users.UDate = DateTime.Now;
                users.UAppear = true;
                users.UNews = false;
                users.UImage = "/Public/img/defaultUser.png";
                users.UStatus = true;
                users.UGender = false;
                users.UPasswd = FormsAuthentication.HashPasswordForStoringInConfigFile(users.UPasswd, "MD5");
                users.UNPasswd = FormsAuthentication.HashPasswordForStoringInConfigFile(users.UNPasswd, "MD5");
                db.Users.Add(users);
                db.SaveChanges();
                return Json(new { status = 1, message = "Kayıt Başarılı.Lütfen Giriş Yapınız." });
            }

            return Json(new { status = 0, message = "Hesap Oluşturulurken Hata Oluştu." });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult Sign(Users user)
        {
            string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(user.UPasswd, "MD5");
            
            var userIn = db.Users.FirstOrDefault(u => user.UNick == u.UNick && pass == u.UPasswd);
            if (userIn != null)
            {
                if (userIn.UStatus == false)
                {
                    return Json(new { status = 0, message = "Kullanıcı Hesabı Askıya Alınmış.Detaylı Bilgi İçin İletişime Geçin." });
                }

                userIn.UNPasswd = pass;
                //Son Giriş Tarihini Güncelliyoruz 
                userIn.ULastSession=DateTime.Now;
                db.Entry(userIn).State = EntityState.Modified;
                db.SaveChanges();
                //Session Oluşturuyoruz
                Session["kullaniciId"] = userIn.UId;
                Session["kullaniciResim"] = userIn.UImage;
                Users dataUser = new Users();
                dataUser.UId = userIn.UId;
                dataUser.UImage = userIn.UImage;
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,userIn.UNick.ToString(),DateTime.Now,DateTime.Now.AddMonths(1),false,
                new JavaScriptSerializer().Serialize(dataUser));
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                Response.Cookies.Add(cookie);
                return Json(new { status = 1, message = "Giriş Başarılı." });
            }

            return Json(new { status = 0, message = "Giriş Başarısız.Şifre veya nickinizi kontrol edin." });
        }

        public ActionResult LogOut()
        {
            string comingUrl = Request.UrlReferrer.ToString();
            FormsAuthentication.SignOut();
            Request.Cookies.Clear();
            Session.Clear();
            Session.Abandon();
            Session.Remove("kullaniciId");
            Session.Remove("kullaniciResim");
            return Redirect(comingUrl);
        }
    }
}
using FilmPortalı.Models;
using FilmPortalı.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            ProfileViewModel vm = new ProfileViewModel();
            int userId = Convert.ToInt16(Session["kullaniciId"]);
            Users user = db.Users.Where(u => u.UId == userId).FirstOrDefault();
            List<Comments> comments = db.Comments.Include("films").Where(c => c.CUId == userId).ToList();
            vm.user = user;
            vm.comments = comments;
            return View(vm);
        }

        [HttpPost]
        public ActionResult UpdateInfo(Users user,string gender)
        {
            var guncellenecekUser = db.Users.Find(user.UId);
            if (guncellenecekUser == null)
            {
                return HttpNotFound();
            }

            guncellenecekUser.UName = user.UName;
            guncellenecekUser.USurname = user.USurname;
            if(user.UBirthDate!=null)
                guncellenecekUser.UBirthDate = user.UBirthDate;
            guncellenecekUser.UGender = gender == "0" ? false : true;
            guncellenecekUser.UTwitter = user.UTwitter;
            guncellenecekUser.UInstagram = user.UInstagram;
            guncellenecekUser.UInfo = user.UInfo;

            db.Entry(guncellenecekUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Content("Başarılı");
        }

        [HttpPost]
        public ActionResult UpdateAccount(Users user,HttpPostedFileBase image)
        {
            var guncellenecekUser = db.Users.Find(user.UId);
            if (guncellenecekUser == null)
            {
                return HttpNotFound();
            }

            guncellenecekUser.UNews = user.UNews;
            guncellenecekUser.UAppear = user.UAppear;
            guncellenecekUser.UPasswd = user.UPasswd;

            if(image != null)
            {
                string rsm = guncellenecekUser.UNick;
                rsm += image.FileName;
                image.SaveAs(Server.MapPath("/Public/img/"+rsm));
                if (guncellenecekUser.UImage != null && guncellenecekUser.UImage.Contains("/Public/img/"))
                    System.IO.File.Delete(Server.MapPath(guncellenecekUser .UImage));

                    guncellenecekUser.UImage = "/Public/img/" + rsm;
                    Session["kullaniciResim"] = "/Public/img/" + rsm;
            } else if(user.UImage != null && !user.UImage.Equals(guncellenecekUser.UImage))
            {
                guncellenecekUser.UImage = user.UImage;
                Session["kullaniciResim"] = user.UImage;
                System.IO.File.Delete(Server.MapPath(guncellenecekUser.UImage));
            }

            db.Entry(guncellenecekUser).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Content("Başarılı");
        }
    }
}
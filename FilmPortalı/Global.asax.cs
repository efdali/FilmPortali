using FilmPortalı.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;
using FilmPortalı.Models;

namespace FilmPortalı
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["totalVisitor"] = 0;
            Application["onlineVisitor"] = 0;
        }

        protected void Session_Start()
        {

            // Session cookie ayarı
            //if (User.Identity.IsAuthenticated)
            //{

            //    HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //    if (cookie != null)
            //    {
            //        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);
            //        var user = new JavaScriptSerializer().Deserialize<Users>(ticket.UserData);
            //        Session["kullaniciId"] = user.UId;
            //        Session["kullaniciResim"] = user.UImage;
            //        cookie.Expires=DateTime.Now.AddDays(1);
            //        Response.Cookies.Add(cookie);
            //    }

            //}

            int onlineVisitor = (int) Application["onlineVisitor"];
            int totalVisitor = (int) Application["totalVisitor"];
            totalVisitor++;
            onlineVisitor++;
            Application.Lock();
            Application["totalVisitor"] = totalVisitor;
            Application["onlineVisitor"] = onlineVisitor;
            Application.UnLock();
        }
        

        protected void Session_End()
        {
            int onlineVisitor = (int) Application["onlineVisitor"];
            onlineVisitor--;
            Application.Lock();
            Application["onlineVisitor"] = onlineVisitor;
            Application.UnLock();
        }

        protected void Application_End()
        {
            Application.Remove("onlineVisitor");
        }

        protected void Application_Error()
        {
            StreamWriter hd = new StreamWriter(Server.MapPath("~/errors.txt"), true);
            //Oluşan hatalar HataDosyasi adlı bir dosyaya kaydediliyor. 
            hd.WriteLine(DateTime.Now.ToString());
            //Server nesnesini GetLastError metodu sunucuda oluşan son hatayı Exception tipinden getirir. Bu da şu an oluşan hata olacaktır. 
            if (Server.GetLastError().InnerException != null)
                hd.WriteLine(Server.GetLastError().InnerException.Message);
            else
                hd.WriteLine(Server.GetLastError().Message);
            //Request nesnesinin Path özelliği şu an istekte bulunulan sayfanın yol bilgisini getirir.
            hd.Write(Request.RawUrl != null ? Request.RawUrl : "");
            hd.WriteLine();
            hd.Close();
        }
    }
}
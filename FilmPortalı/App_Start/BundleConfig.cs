using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace FilmPortalı.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundle)
        {
            bundle.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-3.4.1.min.js",
                "~/Scripts/jquery.modal.min.js",
                "~/Scripts/owl.carousel.min.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js",
                "~/Scripts/main.js",
                "~/Scripts/PNotify.js"
                ));

            bundle.Add(new StyleBundle("~/bundle/css").Include(
                "~/Content/css/normalize.css",
                "~/Content/css/main.css",
                "~/Content/css/owl.carousel.min.css",
                "~/Content/css/jquery.modal.min.css",
                "~/Content/css/PNotify.css"
                ));
        }

    }
}
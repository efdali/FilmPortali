using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPortalı.Areas.Amdin.Controllers
{
    [Authorize(Roles ="A")]
    public class DefaultController : Controller
    {
        // GET: Amdin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilmPortalı.Controllers
{
    public class FilmController : Controller
    {
        // GET: Film
        public ActionResult Index()
        {
            return View();
        }
    }
}
using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmPortalı.Areas.Amdin.ViewModel
{
    public class AddFilmViewModel
    {
        public Films film { get; set; }
        public List<Categories> category { get; set; }
    }
}
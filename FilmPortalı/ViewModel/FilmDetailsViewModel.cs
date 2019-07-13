using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmPortalı.ViewModel
{
    public class FilmDetailsViewModel
    {
        public Films film { get; set; }
        public List<Comments> comment { get; set; }
        public List<Sources> filmSource { get; set; }
        public List<FilmCrew> crew { get; set; }
    }
}
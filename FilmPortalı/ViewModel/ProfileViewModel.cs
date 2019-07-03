using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmPortalı.ViewModel
{
    public class ProfileViewModel
    {
        public List<Comments> comments { get; set; }
        public List<Films> films { get; set; }
        public Users user { get; set; }
    }
}
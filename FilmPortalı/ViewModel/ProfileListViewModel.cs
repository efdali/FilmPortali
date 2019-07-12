using FilmPortalı.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmPortalı.ViewModel
{
    public class ProfileListViewModel
    {
        public List<List> watch { get; set; }
        public List<List> likes { get; set; }
        public List<List> dislikes { get; set; }
    }
}
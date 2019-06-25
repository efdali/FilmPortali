using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmPortalı.Models;

namespace FilmPortalı.ViewModel
{
    public class AnasayfaSliderFilmViewModel
    {
        public List<Slider> slider { get; set; }
        public List<Films> film { get; set; }
    }
}
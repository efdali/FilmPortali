//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FilmPortalı.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Films
    {
        public int FId { get; set; }
        public string FName { get; set; }
        public string FTurkishName { get; set; }
        public string FSummary { get; set; }
        public Nullable<int> FYear { get; set; }
        public string FCountry { get; set; }
        public Nullable<double> FImdb { get; set; }
        public string FPoster { get; set; }
        public string FTrailer { get; set; }
        public string FSeo { get; set; }
        public Nullable<System.DateTime> FCDate { get; set; }
        public Nullable<System.DateTime> FUDate { get; set; }
    }
}

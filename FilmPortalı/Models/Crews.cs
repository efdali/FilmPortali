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
    
    public partial class Crews
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Crews()
        {
            this.FilmCrew = new HashSet<FilmCrew>();
        }
    
        public int CId { get; set; }
        public string CName { get; set; }
        public Nullable<System.DateTime> CBirthday { get; set; }
        public string CPicture { get; set; }
        public string CLife { get; set; }
        public string CSeo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FilmCrew> FilmCrew { get; set; }
    }
}

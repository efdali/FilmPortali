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
    
    public partial class Comments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comments()
        {
            this.SubComments = new HashSet<SubComments>();
        }
    
        public int CId { get; set; }
        public Nullable<int> CUId { get; set; }
        public Nullable<int> CFId { get; set; }
        public string CText { get; set; }
        public Nullable<bool> CStatus { get; set; }
        public Nullable<System.DateTime> CDate { get; set; }
    
        public virtual Films Films { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubComments> SubComments { get; set; }
    }
}

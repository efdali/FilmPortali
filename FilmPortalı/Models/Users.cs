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
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.Comments = new HashSet<Comments>();
            this.List = new HashSet<List>();
            this.Sources = new HashSet<Sources>();
            this.SubComments = new HashSet<SubComments>();
            this.Views = new HashSet<Views>();
        }
    
        public int UId { get; set; }
        public string UNick { get; set; }
        public string UMail { get; set; }
        public string UPasswd { get; set; }
        public string UName { get; set; }
        public string USurname { get; set; }
        public string UInfo { get; set; }
        public string UImage { get; set; }
        public Nullable<System.DateTime> UBirthDate { get; set; }
        public Nullable<bool> UGender { get; set; }
        public string UTwitter { get; set; }
        public string UInstagram { get; set; }
        public bool UAppear { get; set; }
        public bool UNews { get; set; }
        public Nullable<System.DateTime> UDate { get; set; }
        public Nullable<bool> UStatus { get; set; }
        public string URole { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<List> List { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sources> Sources { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubComments> SubComments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Views> Views { get; set; }
    }
}

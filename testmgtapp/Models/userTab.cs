//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace testmgtapp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class userTab
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public userTab()
        {
            this.answerTabs = new HashSet<answerTab>();
            this.assignSubTeacherTabs = new HashSet<assignSubTeacherTab>();
        }
    
        public int uId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Nullable<int> roleId { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> cId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<answerTab> answerTabs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assignSubTeacherTab> assignSubTeacherTabs { get; set; }
        public virtual courseTab courseTab { get; set; }
        public virtual roleTab roleTab { get; set; }
    }
}

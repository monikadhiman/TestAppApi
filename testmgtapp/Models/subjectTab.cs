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
    
    public partial class subjectTab
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public subjectTab()
        {
            this.assignSubTeacherTabs = new HashSet<assignSubTeacherTab>();
            this.testTabs = new HashSet<testTab>();
        }
    
        public int subId { get; set; }
        public string subName { get; set; }
        public Nullable<bool> isActive { get; set; }
        public Nullable<int> cId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assignSubTeacherTab> assignSubTeacherTabs { get; set; }
        public virtual courseTab courseTab { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<testTab> testTabs { get; set; }
    }
}
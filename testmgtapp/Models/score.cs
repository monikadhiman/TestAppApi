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
    
    public partial class score
    {
        public int scoreId { get; set; }
        public string email { get; set; }
        public Nullable<int> score1 { get; set; }
        public Nullable<int> timespend { get; set; }
        public Nullable<int> tstId { get; set; }
    
        public virtual testTab testTab { get; set; }
    }
}
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
    
    public partial class answerTab
    {
        public int ansId { get; set; }
        public Nullable<int> quesId { get; set; }
        public Nullable<int> answer { get; set; }
        public Nullable<int> uId { get; set; }
    
        public virtual questionTab questionTab { get; set; }
        public virtual userTab userTab { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeamProjects.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class timetable_park
    {
        public timetable_park()
        {
            this.timetable_request = new HashSet<timetable_request>();
        }
    
        public int Park_ID { get; set; }
        public string Park_Name { get; set; }
    
        public virtual ICollection<timetable_request> timetable_request { get; set; }
    }
}

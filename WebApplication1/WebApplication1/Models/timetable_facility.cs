//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class timetable_facility
    {
        public timetable_facility()
        {
            this.timetable_request_facility = new HashSet<timetable_request_facility>();
        }
    
        public byte Facility_ID { get; set; }
        public string Facility_Name { get; set; }
    
        public virtual ICollection<timetable_request_facility> timetable_request_facility { get; set; }
    }
}

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
    
    public partial class timetable_request_facility
    {
        public short Request_ID { get; set; }
        public byte Facility_ID { get; set; }
        public byte Quantity { get; set; }
    
        public virtual timetable_facility timetable_facility { get; set; }
    }
}
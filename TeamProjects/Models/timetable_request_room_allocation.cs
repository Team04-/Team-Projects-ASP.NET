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
    
    public partial class timetable_request_room_allocation
    {
        public short Request_ID { get; set; }
        public string Building_ID { get; set; }
        public string Room_ID { get; set; }
        public int Park_ID { get; set; }
    
        public virtual timetable_request timetable_request { get; set; }
        public virtual timetable_request_room_allocation timetable_request_room_allocation1 { get; set; }
        public virtual timetable_request_room_allocation timetable_request_room_allocation2 { get; set; }
        public virtual timetable_room timetable_room { get; set; }
    }
}

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
    
    public partial class timetable_request
    {
        public short Request_ID { get; set; }
        public string Department_Code { get; set; }
        public string Module_Code { get; set; }
        public byte Day_ID { get; set; }
        public int Park_ID { get; set; }
        public byte Start_Time { get; set; }
        public byte Duration { get; set; }
        public int Number_Students { get; set; }
        public byte Number_Rooms { get; set; }
        public bool Priority { get; set; }
        public string Custom_Comments { get; set; }
        public short Current_Round { get; set; }
        public byte Request_Status { get; set; }
    
        public virtual timetable_park timetable_park { internal get; set; }
        public virtual timetable_request_room_allocation timetable_request_room_allocation { internal get; set; }
        public virtual timetable_request_week timetable_request_week { internal get; set; }
        public virtual timetable_round timetable_round { internal get; set; }
    }
}

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
    
    public partial class timetable_room
    {
        public timetable_room()
        {
            this.timetable_request_room_allocation = new HashSet<timetable_request_room_allocation>();
        }
    
        public Nullable<int> Park_ID { get; set; }
        public string Building_ID { get; set; }
        public string Room_ID { get; set; }
        public Nullable<int> Capacity { get; set; }
        public byte Type_ID { get; set; }
    
        public virtual ICollection<timetable_request_room_allocation> timetable_request_room_allocation { get; set; }
        public virtual timetable_room timetable_room1 { get; set; }
        public virtual timetable_room timetable_room2 { get; set; }
        public virtual timetable_room_type timetable_room_type { get; set; }
    }
}

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
    using System.ComponentModel.DataAnnotations;
    
    public partial class timetable_room
    {
        public timetable_room()
        {
            this.timetable_request_room_allocation = new HashSet<timetable_request_room_allocation>();
        }
        [Required]
        public string Building_ID { get; set; }
        [Required]
        public string Room_ID { get; set; }
        [Required]
        public Nullable<int> Capacity { get; set; }
        [Required]
        public byte Type_ID { get; set; }
    
        public virtual ICollection<timetable_request_room_allocation> timetable_request_room_allocation { get; internal set; }
        public virtual timetable_room_type timetable_room_type { get; internal set; }
    }
}

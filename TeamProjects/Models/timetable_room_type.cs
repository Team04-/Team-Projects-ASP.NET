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
    
    public partial class timetable_room_type
    {
        public timetable_room_type()
        {
            this.timetable_room = new HashSet<timetable_room>();
        }
    
        public byte Type_ID { get; set; }
        public string Type_Name { get; set; }
    
        public virtual ICollection<timetable_room> timetable_room { get; set; }
    }
}

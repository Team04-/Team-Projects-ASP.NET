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
    
    public partial class timetable_round
    {
        public timetable_round()
        {
            this.timetable_department = new HashSet<timetable_department>();
            this.timetable_request = new HashSet<timetable_request>();
        }
    
        public short Round_Code { get; set; }
        public string Round_Status { get; set; }
        public short Semester { get; set; }
        public System.DateTime Start_Date { get; set; }
        public System.DateTime End_Date { get; set; }
    
        public virtual ICollection<timetable_department> timetable_department { get; set; }
        public virtual ICollection<timetable_request> timetable_request { get; set; }
    }
}

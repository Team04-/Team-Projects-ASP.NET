using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TeamProjects.Models
{
    public class RequestListModel
    {
        public int Request_ID { get; set; }
        public string Module_Code { get; set; }
        public string Module_Title { get; set; }
        public int Number_Rooms { get; set; }
        public string[] Rooms { get; set; }
        public string Custom_Comments { get; set; }
        public string Day { get; set; }
        public int Park { get; set; }
        public int Start_Period { get; set; }
        public int End_Period { get; set; }
        public int Number_Students { get; set; }
        public bool Priority { get; set; }
        public bool Has_Comments { get; set; }
        public int Round { get; set; }
        public int Status { get; set; }
        public bool[] Weeks { get; set; }
    }
}
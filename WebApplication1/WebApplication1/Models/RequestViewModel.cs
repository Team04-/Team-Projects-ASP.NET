using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RequestViewModel
    {
            public int Request_ID { get; set; }
            public string Department_Code { get; set; }
            public char Part_Code { get; set; }
            public string Module_Code { get; set; }
            public int Day_ID { get; set; }
            public int Start_Time { get; set; }
            public int Duration { get; set; }
            public int Number_Students { get; set; }
            public int Number_Rooms { get; set; }
            public int Priority { get; set; }
            public int Room_Type { get; set; }
            public int Park_ID { get; set; }
            public string Custom_Comments { get; set; }
            public int Current_Year { get; set; }
            public int Current_Semester { get; set; }
            public int Current_Round { get; set; }
            public int Request_Status { get; set; }
    }
}
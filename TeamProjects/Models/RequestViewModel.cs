using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace TeamProjects.Models
{

    public class RequestViewModel
    {
        // Tinyint in DB --> byte in ASP.NET
        public int Request_ID { get; set; }
        public string Department_Code { get; set; }
        //public char Part_Code { get; set; }
        public string Module_Code { get; set; }
        public string Room_Pref_JSON { get; set; }
        public int Day_ID { get; set; }
        public int Start_Time { get; set; }
        public int Duration { get; set; }
        public int Number_Students { get; set; }
        public bool Priority { get; set; }
        public string Room_Type { get; set; }
        public int Park_ID { get; set; }
		public int Park_ID2 { get; set; }
        public string Custom_Comments { get; set; }
        public int Current_Year { get; set; }
        public byte Current_Semester { get; set; }
        public byte Current_Round { get; set; }
        //public int Request_Status { get; set; }
        public int Facility_ID { get; set; }
        public int Room_ID { get; set; }
        public int Building_ID { get; set; }
        public bool[] Week { get; set; }

        public byte Number_Rooms { get; set; }
        public bool WeekOne { get; set; }
        public bool WeekTwo { get; set; }
        public bool WeekThree { get; set; }
        public bool WeekFour { get; set; }
        public bool WeekFive { get; set; }
        public bool WeekSix { get; set; }
        public bool WeekSeven { get; set; }
        public bool WeekEight { get; set; }
        public bool WeekNine { get; set; }
        public bool WeekTen { get; set; }
        public bool WeekEleven { get; set; }
        public bool WeekTwelve { get; set; }
        public bool WeekThirteen { get; set; }
        public bool WeekFourteen { get; set; }
        public bool WeekFifteen { get; set; }
    }

    public class RoomPref {
        public string Room_Type;
        public string Park;
        public string Building;
        public string Room;
    }

}

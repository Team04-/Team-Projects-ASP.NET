using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class RoomBuildingViewModel
    {
        public string Building_Name { get; set; }
        public string Building_ID { get; set; }
        public string Room_ID { get; set; }
        public int Capacity { get; set; }
        public int Park_ID { get; set; }
        public byte Type_ID { get; set; }
    }
}
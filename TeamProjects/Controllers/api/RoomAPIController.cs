using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TeamProjects.Models;

namespace TeamProjects.Controllers.api
{
    public class RoomAPIController : ApiController
    {
        private team04Entities db = new team04Entities();

        // GET api/RoomAPIController
        public IEnumerable<timetable_room> Gettimetable_room()
        {
			return db.timetable_room;
        }

		// GET api/RoomAPIController/5
		public IQueryable<string> Gettimetable_room(string BuildingID, string RoomType, string FacIdString)
		{
			var timetable_room_type = (from m in db.timetable_room_type where m.Type_Name == RoomType select m.Type_ID).ToList();

			byte selID = timetable_room_type.FirstOrDefault();

			IQueryable<timetable_room> timetable_room;


			if (BuildingID == "N/A") {
				timetable_room = from m in db.timetable_room where m.Type_ID == selID select m;
			}

			else
			{
				timetable_room = from m in db.timetable_room where m.Building_ID == BuildingID && m.Type_ID == selID select m;

			}

			int facIdInt;

            //Filter by the first facility
			if (FacIdString.Length > 1)
            {
				string[] facIdArray = (FacIdString.Remove(FacIdString.Length - 1, 1)).Split('|');

				facIdInt = Convert.ToInt32(facIdArray[0]);

				IQueryable<timetable_room_facility> timetable_room_facility = from m in db.timetable_room_facility where m.Facility_ID == facIdInt select m;
					//db.timetable_room_facility.Where(m => m.Facility_ID == Convert.ToInt32(facIdArray[0]));

                for (var i = 1; i < facIdArray.Length; i++)
                {
					facIdInt = Convert.ToInt32(facIdArray[i]);
					timetable_room_facility = timetable_room_facility.Where(f => f.Facility_ID == facIdInt);
                }

				IQueryable<string> result = from r in timetable_room_facility join f in timetable_room on r.Room_ID equals f.Room_ID select r.Room_ID;

				result = result.Distinct();

				return result;
            }

            else
            {
				IQueryable<string> result = from r in timetable_room select r.Room_ID;
                return result;
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
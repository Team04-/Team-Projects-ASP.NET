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
			//return Json(db.timetable_room.AsEnumerable(), JsonRequestBehavior.AllowGet);
			return db.timetable_room;
        }

        // GET api/RoomAPIController/5
        //public IEnumerable<timetable_room> Gettimetable_room(string BuildingID)
        //{

			//var timetable_room = from m in db.timetable_room select m;
			//timetable_room = timetable_room.Where(e => e.Building_ID.Equals(BuildingID));

            //if (timetable_room == null)
            //{
                //throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            //}

            //return timetable_room;
        //}

		// GET api/RoomAPIController/5
		public IEnumerable<timetable_room> Gettimetable_room(string BuildingID, string RoomType, string FacIdString)
		{
			var timetable_room_type = (from m in db.timetable_room_type where m.Type_Name == RoomType select m.Type_ID).ToList();

			byte selID = timetable_room_type.FirstOrDefault();

			var timetable_room = from m in db.timetable_room where m.Building_ID == BuildingID && m.Type_ID == selID select m;

            string[] facIdArray = FacIdString.Split('|');

            //Filter by the first facility
            if (facIdArray.Length > 0)
            {
                IQueryable<timetable_room_facility> timetable_room_facility = db.timetable_room_facility.Where(m => m.Facility_ID == Convert.ToInt32(facIdArray[0])).AsQueryable<timetable_room_facility>();

                for (var i = 1; i < facIdArray.Length; i++)
                {
                    timetable_room_facility = timetable_room_facility.Where(f => f.Facility_ID == Convert.ToInt32(facIdArray[i]));
                }

                var result = from r in timetable_room
                             join f in timetable_room_facility on r.Room_ID equals f.Room_ID
                             select r;

                return result;
            }

            else
            {
                return timetable_room;
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
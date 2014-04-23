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
    public class RoomAPI : ApiController
    {
        private team04Entities db = new team04Entities();

        // GET api/RoomAPI
        public IEnumerable<timetable_room> Gettimetable_room()
        {
            return db.timetable_room.AsEnumerable();
        }

        // GET api/RoomAPI/5
        public IEnumerable<timetable_room> Gettimetable_room(string BuildingID)
        {
            IEnumerable<timetable_room> timetable_room = db.timetable_room.Where(bc => bc.Building_ID == BuildingID);
            if (timetable_room == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return timetable_room;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
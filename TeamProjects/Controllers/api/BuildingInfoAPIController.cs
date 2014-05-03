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
    public class BuildingInfoAPIController : ApiController
    {
        private team04Entities db = new team04Entities();

        // GET api/BuildingInfoAPI
        public IEnumerable<timetable_building> Gettimetable_building()
        {
            return db.timetable_building.AsEnumerable();
        }

        // GET api/BuildingInfoAPI/5
        public timetable_building Gettimetable_building(string id)
        {
            timetable_building timetable_building = db.timetable_building.Find(id);
            if (timetable_building == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return timetable_building;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
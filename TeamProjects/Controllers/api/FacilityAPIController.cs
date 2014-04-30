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
    public class facilityAPIController : ApiController
    {
        private team04Entities db = new team04Entities();

        // GET api/facilityAPIController
        public IEnumerable<timetable_facility> Gettimetable_facility()
        {
			//return Json(db.timetable_facility.AsEnumerable(), JsonRequestBehavior.AllowGet);
			return db.timetable_facility;
        }

		// GET api/facilityAPIController/5

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
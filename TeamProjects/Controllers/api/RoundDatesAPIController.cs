using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeamProjects.Models;

namespace TeamProjects.Controllers.api
{
    public class RoundDatesAPIController : ApiController
    {
        private team04Entities db = new team04Entities();

        // GET api/buildingAPIController
        public timetable_round Gettimetable_round()
        {
            //return Json(db.timetable_building.AsEnumerable(), JsonRequestBehavior.AllowGet);
            return db.timetable_round.Where(r => r.Round_Status == "Current").ToList().FirstOrDefault();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

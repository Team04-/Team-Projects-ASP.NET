using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeamProjects.Models;

namespace TeamProjects.Controllers.api
{
    public class RoundAPIController : ApiController
    {
        private team04Entities db = new team04Entities();

        // GET api/RoundAPIController
        public timetable_round Gettimetable_requests()
        {
            //return Json(db.timetable_room.AsEnumerable(), JsonRequestBehavior.AllowGet);
            var check = db.timetable_round.Where(r => r.Round_Status == "Current").First();
            if (!(check is  timetable_round))
            {
                check = db.timetable_round.Last();
            }
            return check;
        }

        // GET api/RoundAPIController/5
        public timetable_round Gettimetable_requests(short RoundCode)
        {

            var timetable_round = db.timetable_round.Where(r => r.Round_Code == RoundCode).First();

            if (timetable_round == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return timetable_round;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

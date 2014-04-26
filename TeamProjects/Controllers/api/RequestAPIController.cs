using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeamProjects.Models;

namespace TeamProjects.Controllers.api
{
    public class RequestAPIController : ApiController
    {
        private team04Entities db = new team04Entities();

        // GET api/RequestAPIController
        public IEnumerable<timetable_request> Gettimetable_requests()
        {
            //return Json(db.timetable_room.AsEnumerable(), JsonRequestBehavior.AllowGet);
            return db.timetable_request;
        }

        // GET api/RequestAPIController/5
        public IEnumerable<timetable_request> Gettimetable_requests(string RequestID)
        {

            var timetable_request = from m in db.timetable_request select m;
            timetable_request = timetable_request.Where(e => e.Request_ID.Equals(RequestID));

            if (timetable_request == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return timetable_request;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

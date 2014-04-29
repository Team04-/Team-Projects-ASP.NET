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

namespace TeamProjects.Controllers
{
    public class moduleAPIController : ApiController
    {
        private team04Entities db = new team04Entities();

        // GET api/moduleAPIController
        public IEnumerable<timetable_module> Gettimetable_module()
        {
            //return Json(db.timetable_module.AsEnumerable(), JsonRequestBehavior.AllowGet);
            return db.timetable_module;
        }

        // GET api/moduleAPIController/5
        public IEnumerable<timetable_module> Gettimetable_module(string DeptCode, string PartCode)
        {

            var timetable_module = (from m in db.timetable_module where m.Department_Code == DeptCode && m.Part_Code == PartCode select m);

            if (timetable_module == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return timetable_module;
        }

        public IEnumerable<timetable_module> Gettimetable_modulePartCode(string DeptCode2)
        {

            var timetable_module = (IEnumerable<timetable_module>) (from m in db.timetable_module where m.Department_Code == DeptCode2 select m.Part_Code).Distinct();

            if (timetable_module == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return timetable_module;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
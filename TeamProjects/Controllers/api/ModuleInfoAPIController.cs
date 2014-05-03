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
    public class ModuleInfoAPIController : ApiController
    {
        private team04Entities db = new team04Entities();

        // GET api/ModuleInfoAPI
        public IEnumerable<timetable_module> Gettimetable_module()
        {
            return db.timetable_module.AsEnumerable();
        }

        // GET api/ModuleInfoAPI/5
        public timetable_module Gettimetable_module(string deptCode,string modCode)
        {
            timetable_module timetable_module = db.timetable_module.Where(m => m.Department_Code == deptCode).Where(m => m.Module_Code == modCode).First();
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
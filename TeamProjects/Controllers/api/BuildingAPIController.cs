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
	public class BuildingAPIController : ApiController
	{
		private team04Entities db = new team04Entities();

		// GET api/buildingAPIController
		public IEnumerable<timetable_building> Gettimetable_building()
		{
			//return Json(db.timetable_building.AsEnumerable(), JsonRequestBehavior.AllowGet);
			return db.timetable_building;
		}

		// GET api/buildingAPIController/5
		public IEnumerable<timetable_building> Gettimetable_building(string ParkID)
		{

			var timetable_building = from m in db.timetable_building select m;
			timetable_building = timetable_building.Where(e => e.Park_ID.Equals(ParkID));

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
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProjects.Models;

namespace TeamProjects.Controllers.manage
{
    public class RequestController : Controller
    {
        private team04Entities db = new team04Entities();

        // GET: /Request/
        public ActionResult Index()
        {
            string query = "SELECT dept.Department_Code as Department_Code, mod.Part_Code as Part_Code, mod.Module_Code as Module_Code, roomt.Type_Name as Room_Type, park.Park_ID as Park_ID from timetable_department dept, timetable_request mod, timetable_room_type roomt, timetable_park park";
            var viewModel = db.Database.SqlQuery<TeamProjects.Models.RequestViewModel>(query);
            return View(viewModel.ToList());
        }

        // GET: /Request/Details/5
        public ActionResult Details(string id = null)
        {
            timetable_request timetable_request = db.timetable_request.Find(id);
            if (timetable_request == null)
            {
                return HttpNotFound();
            }
            return View(timetable_request);
        }

        // GET: /Request/Create
        public ActionResult Create()
        {
            ViewBag.Start_Time = new SelectList(new[] { "1/T", "2/T", "3/T", "4/T", "5/T", "6/T", "7/T", "8/T", "9/T" });
            ViewBag.Duration = new SelectList(new[] { "1/T", "2/T", "3/T", "4/T", "5/T", "6/T", "7/T", "8/T", "9/T" });
            ViewBag.Department_Code = new SelectList(db.timetable_department, "Department_Code", "Department_Name");
            ViewBag.Part_Code = new SelectList(db.timetable_request, "Part_Code", "Part_Code");
            ViewBag.Module_Code = new SelectList(db.timetable_request, "Module_Code", "Module_Title");
            ViewBag.Day_ID = new SelectList(db.timetable_day, "Day_ID", "Day_Name");
            ViewBag.Park_ID = new SelectList(db.timetable_park, "Park_ID", "Park_Name");
			ViewBag.Building_ID = new SelectList(db.timetable_building, "Building_ID", "Building_Name");
			ViewBag.Room_ID = new SelectList(db.timetable_room, "Room_ID", "Room_ID");
			ViewBag.Facility_ID = new SelectList(db.timetable_facility, "Facility_ID", "Facility_Name");
			ViewBag.Room_Type = new SelectList(db.timetable_room_type, "Type_ID", "Type_Name");

            return View();
        }

		public bool GetRoomPrefCollection(string[][] roomPrefColl)
		{
			return roomPrefColl != null;
		}

        // POST: /Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Request_ID,Department_Code,Part_Code,Module_Code,Day_ID,Start_Time,Duration,Number_Students,Number_Rooms,Priority,Room_Type,Park_ID,Custom_Comments,Current_Year,Current_Semester,Current_Round,Request_Status")] RequestViewModel requestView)
        {
            // Make new timetable_request object, add requestView ("master data") Current_Round value.
            // Do this for all tables which need subsets of data from requestView.

			string query = "SELECT dept.Department_Code as Department_Code, mod.Part_Code as Part_Code, mod.Module_Code as Module_Code, roomt.Type_Name as Room_Type, park.Park_ID as Park_ID from timetable_department dept, timetable_request mod, timetable_room_type roomt, timetable_park park";
			var viewModel = db.Database.SqlQuery<TeamProjects.Models.RequestViewModel>(query);
			return View(viewModel.ToList());
        }

        // GET: /Request/Edit/5
        public ActionResult Edit(string id = null)
        {
            timetable_request timetable_request = db.timetable_request.Find(id);
            if (timetable_request == null)
            {
                return HttpNotFound();
            }
            return View(timetable_request);
        }

        // POST: /Request/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Request_ID,Department_Code,Part_Code,Module_Code,Day_ID,Start_Time,Duration,Number_Students,Number_Rooms,Priority,Room_Type,Park_ID,Custom_Comments,Current_Year,Current_Semester,Current_Round,Request_Status")] timetable_request timetable_request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable_request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Day_ID = new SelectList(db.timetable_day, "Day_ID", "Day_Name", timetable_request.Day_ID);
            ViewBag.Department_Code = new SelectList(db.timetable_request, "Department_Code", "Module_Title", timetable_request.Department_Code);
            ViewBag.Request_ID = new SelectList(db.timetable_request, "Request_ID", "Department_Code", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request, "Request_ID", "Department_Code", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request_room_allocation, "Request_ID", "Building_ID", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request_week, "Request_ID", "Request_ID", timetable_request.Request_ID);
            return View(timetable_request);
        }

        // GET: /Request/Delete/5
        public ActionResult Delete(string id = null)
        {
            timetable_request timetable_request = db.timetable_request.Find(id);
            if (timetable_request == null)
            {
                return HttpNotFound();
            }
            return View(timetable_request);
        }

        // POST: /Request/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            timetable_request timetable_request = db.timetable_request.Find(id);
            db.timetable_request.Remove(timetable_request);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
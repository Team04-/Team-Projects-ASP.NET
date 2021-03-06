﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers.manage
{
    public class RequestController : Controller
    {
        private TestDBEntities db = new TestDBEntities();

        // GET: /Request/
        public ActionResult Index()
        {
            var timetable_request = db.timetable_request.Include(t => t.timetable_day).Include(t => t.timetable_module).Include(t => t.timetable_request1).Include(t => t.timetable_request2).Include(t => t.timetable_request_room_allocation).Include(t => t.timetable_request_week);
            return View(timetable_request.ToList());
        }

        // GET: /Request/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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
            ViewBag.Day_ID = new SelectList(db.timetable_day, "Day_ID", "Day_Name");
            ViewBag.Department_Code = new SelectList(db.timetable_module, "Department_Code", "Module_Title");
            ViewBag.Request_ID = new SelectList(db.timetable_request, "Request_ID", "Department_Code");
            ViewBag.Request_ID = new SelectList(db.timetable_request, "Request_ID", "Department_Code");
            ViewBag.Request_ID = new SelectList(db.timetable_request_room_allocation, "Request_ID", "Building_ID");
            ViewBag.Request_ID = new SelectList(db.timetable_request_week, "Request_ID", "Request_ID");
            return View();
        }

        // POST: /Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Request_ID,Department_Code,Part_Code,Module_Code,Day_ID,Start_Time,Duration,Number_Students,Number_Rooms,Priority,Room_Type,Park_ID,Custom_Comments,Current_Year,Current_Semester,Current_Round,Request_Status")] timetable_request timetable_request)
        {
            if (ModelState.IsValid)
            {
                db.timetable_request.Add(timetable_request);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Day_ID = new SelectList(db.timetable_day, "Day_ID", "Day_Name", timetable_request.Day_ID);
            ViewBag.Department_Code = new SelectList(db.timetable_module, "Department_Code", "Module_Title", timetable_request.Department_Code);
            ViewBag.Request_ID = new SelectList(db.timetable_request, "Request_ID", "Department_Code", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request, "Request_ID", "Department_Code", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request_room_allocation, "Request_ID", "Building_ID", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request_week, "Request_ID", "Request_ID", timetable_request.Request_ID);
            return View(timetable_request);
        }

        // GET: /Request/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable_request timetable_request = db.timetable_request.Find(id);
            if (timetable_request == null)
            {
                return HttpNotFound();
            }
            ViewBag.Day_ID = new SelectList(db.timetable_day, "Day_ID", "Day_Name", timetable_request.Day_ID);
            ViewBag.Department_Code = new SelectList(db.timetable_module, "Department_Code", "Module_Title", timetable_request.Department_Code);
            ViewBag.Request_ID = new SelectList(db.timetable_request, "Request_ID", "Department_Code", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request, "Request_ID", "Department_Code", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request_room_allocation, "Request_ID", "Building_ID", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request_week, "Request_ID", "Request_ID", timetable_request.Request_ID);
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
            ViewBag.Department_Code = new SelectList(db.timetable_module, "Department_Code", "Module_Title", timetable_request.Department_Code);
            ViewBag.Request_ID = new SelectList(db.timetable_request, "Request_ID", "Department_Code", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request, "Request_ID", "Department_Code", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request_room_allocation, "Request_ID", "Building_ID", timetable_request.Request_ID);
            ViewBag.Request_ID = new SelectList(db.timetable_request_week, "Request_ID", "Request_ID", timetable_request.Request_ID);
            return View(timetable_request);
        }

        // GET: /Request/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
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

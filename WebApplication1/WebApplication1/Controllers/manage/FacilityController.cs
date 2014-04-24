using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team_Projects.Models;

namespace Team_Projects.Controllers.manage
{
    public class FacilityController : Controller
    {
        private team04Entities db = new team04Entities();

        //
        // GET: /Facility/

        public ActionResult Index()
        {
            return View(db.timetable_facility.ToList());
        }

        //
        // GET: /Facility/Details/5

        public ActionResult Details(byte id = 0)
        {
            timetable_facility timetable_facility = db.timetable_facility.Single(t => t.Facility_ID == id);
            if (timetable_facility == null)
            {
                return HttpNotFound();
            }
            return View(timetable_facility);
        }

        //
        // GET: /Facility/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Facility/Create

        [HttpPost]
        public ActionResult Create(timetable_facility timetable_facility)
        {
            if (ModelState.IsValid)
            {
                db.timetable_facility.AddObject(timetable_facility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_facility);
        }

        //
        // GET: /Facility/Edit/5

        public ActionResult Edit(byte id = 0)
        {
            timetable_facility timetable_facility = db.timetable_facility.Single(t => t.Facility_ID == id);
            if (timetable_facility == null)
            {
                return HttpNotFound();
            }
            return View(timetable_facility);
        }

        //
        // POST: /Facility/Edit/5

        [HttpPost]
        public ActionResult Edit(timetable_facility timetable_facility)
        {
            if (ModelState.IsValid)
            {
                db.timetable_facility.Attach(timetable_facility);
                db.ObjectStateManager.ChangeObjectState(timetable_facility, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_facility);
        }

        //
        // GET: /Facility/Delete/5

        public ActionResult Delete(byte id = 0)
        {
            timetable_facility timetable_facility = db.timetable_facility.Single(t => t.Facility_ID == id);
            if (timetable_facility == null)
            {
                return HttpNotFound();
            }
            return View(timetable_facility);
        }

        //
        // POST: /Facility/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(byte id)
        {
            timetable_facility timetable_facility = db.timetable_facility.Single(t => t.Facility_ID == id);
            db.timetable_facility.DeleteObject(timetable_facility);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
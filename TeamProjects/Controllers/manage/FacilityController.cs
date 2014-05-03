using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProjects.Models;

namespace TeamProjects.Controllers.manage
{
    public class FacilityController : Controller
    {
        private team04Entities db = new team04Entities();

        //
        // GET: /Facility/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.timetable_facility.ToList());
        }

        //
        // GET: /Facility/Details/5

        public ActionResult Details(byte id = 0)
        {
            timetable_facility timetable_facility = db.timetable_facility.Find(id);
            if (timetable_facility == null)
            {
                return HttpNotFound();
            }
            return View(timetable_facility);
        }

        //
        // GET: /Facility/Create
        [Authorize]
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
                db.timetable_facility.Add(timetable_facility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_facility);
        }

        //
        // GET: /Facility/Edit/5
        [Authorize]
        public ActionResult Edit(byte id = 0)
        {
            timetable_facility timetable_facility = db.timetable_facility.Find(id);
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
                db.Entry(timetable_facility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_facility);
        }

        //
        // GET: /Facility/Delete/5
        [Authorize]
        public ActionResult Delete(byte id = 0)
        {
            timetable_facility timetable_facility = db.timetable_facility.Find(id);
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
            timetable_facility timetable_facility = db.timetable_facility.Find(id);
            db.timetable_facility.Remove(timetable_facility);
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
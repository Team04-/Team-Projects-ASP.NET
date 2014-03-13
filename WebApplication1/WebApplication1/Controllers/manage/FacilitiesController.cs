using System;
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
    public class FacilitiesController : Controller
    {
        private TestDBEntities db = new TestDBEntities();

        // GET: /Facilities/
        public ActionResult Index()
        {
            return View(db.timetable_facility.ToList());
        }

        // GET: /Facilities/Details/5
        public ActionResult Details(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable_facility timetable_facility = db.timetable_facility.Find(id);
            if (timetable_facility == null)
            {
                return HttpNotFound();
            }
            return View(timetable_facility);
        }

        // GET: /Facilities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Facility_ID,Facility_Name")] timetable_facility timetable_facility)
        {
            if (ModelState.IsValid)
            {
                db.timetable_facility.Add(timetable_facility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_facility);
        }

        // GET: /Facilities/Edit/5
        public ActionResult Edit(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable_facility timetable_facility = db.timetable_facility.Find(id);
            if (timetable_facility == null)
            {
                return HttpNotFound();
            }
            return View(timetable_facility);
        }

        // POST: /Facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Facility_ID,Facility_Name")] timetable_facility timetable_facility)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable_facility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_facility);
        }

        // GET: /Facilities/Delete/5
        public ActionResult Delete(byte? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable_facility timetable_facility = db.timetable_facility.Find(id);
            if (timetable_facility == null)
            {
                return HttpNotFound();
            }
            return View(timetable_facility);
        }

        // POST: /Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(byte id)
        {
            timetable_facility timetable_facility = db.timetable_facility.Find(id);
            db.timetable_facility.Remove(timetable_facility);
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

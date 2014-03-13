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
    public class BuildingController : Controller
    {
        private TestDBEntities db = new TestDBEntities();

        // GET: /Building/
        public ActionResult Index()
        {
            return View(db.timetable_building.ToList());
        }

        // GET: /Building/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable_building timetable_building = db.timetable_building.Find(id);
            if (timetable_building == null)
            {
                return HttpNotFound();
            }
            return View(timetable_building);
        }

        // GET: /Building/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Building/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Building_ID,Building_Name")] timetable_building timetable_building)
        {
            if (ModelState.IsValid)
            {
                db.timetable_building.Add(timetable_building);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_building);
        }

        // GET: /Building/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable_building timetable_building = db.timetable_building.Find(id);
            if (timetable_building == null)
            {
                return HttpNotFound();
            }
            return View(timetable_building);
        }

        // POST: /Building/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Building_ID,Building_Name")] timetable_building timetable_building)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable_building).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_building);
        }

        // GET: /Building/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable_building timetable_building = db.timetable_building.Find(id);
            if (timetable_building == null)
            {
                return HttpNotFound();
            }
            return View(timetable_building);
        }

        // POST: /Building/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            timetable_building timetable_building = db.timetable_building.Find(id);
            db.timetable_building.Remove(timetable_building);
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

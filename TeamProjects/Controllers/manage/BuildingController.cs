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
    public class BuildingController : Controller
    {
        private team04Entities db = new team04Entities();

        //
        // GET: /Building/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.timetable_building.ToList());
        }

        //
        // GET: /Building/Details/5
        [Authorize]
        public ActionResult Details(string id = null)
        {
            timetable_building timetable_building = db.timetable_building.Find(id);
            if (timetable_building == null)
            {
                return HttpNotFound();
            }
            return View(timetable_building);
        }

        //
        // GET: /Building/Create
        [Authorize]
        public ActionResult Create()
        {
            List<timetable_park> parks = db.timetable_park.ToList();
            List<int> parkIDs = new List<int>();
            List<string> parkNames = new List<string>();
            foreach (timetable_park park in parks) {
                parkIDs.Add(park.Park_ID);
                parkNames.Add(park.Park_Name);
            }
            ViewBag.parkIDs = parkIDs;
            ViewBag.parkNames = parkNames;
            return View();
        }

        //
        // POST: /Building/Create

        [HttpPost]
        public ActionResult Create(timetable_building timetable_building)
        {
            if (ModelState.IsValid)
            {
                db.timetable_building.Add(timetable_building);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_building);
        }

        //
        // GET: /Building/Edit/5
        [Authorize]
        public ActionResult Edit(string id = null)
        {
            timetable_building timetable_building = db.timetable_building.Find(id);
            if (timetable_building == null)
            {
                return HttpNotFound();
            }
            return View(timetable_building);
        }

        //
        // POST: /Building/Edit/5

        [HttpPost]
        public ActionResult Edit(timetable_building timetable_building)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable_building).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_building);
        }

        //
        // GET: /Building/Delete/5
        [Authorize]
        public ActionResult Delete(string id = null)
        {
            timetable_building timetable_building = db.timetable_building.Find(id);
            if (timetable_building == null)
            {
                return HttpNotFound();
            }
            return View(timetable_building);
        }

        //
        // POST: /Building/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            timetable_building timetable_building = db.timetable_building.Find(id);
            db.timetable_building.Remove(timetable_building);
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
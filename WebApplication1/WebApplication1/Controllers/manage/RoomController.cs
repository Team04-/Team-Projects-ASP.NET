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
    public class RoomController : Controller
    {
        private TestDBEntities db = new TestDBEntities();

        // GET: /Room/
        public ActionResult Index()
        {
            string query = "SELECT b.Building_Name as Building_Name, r.Building_ID as Building_ID, r.Park_ID as Park_ID, r.Room_ID as Room_ID, r.Capacity as Capacity, r.Type_ID as Type_ID from timetable_building b, timetable_room r WHERE b.Building_ID = r.Building_ID";
            var viewModel = db.Database.SqlQuery<WebApplication1.Models.RoomBuildingViewModel>(query);
            return View(viewModel.ToList());
        }

        // GET: /Room/Details/5
        public ActionResult Details(string buildingID, string roomID)
        {
            if (roomID == null || buildingID == null)
            {
                return RedirectToAction("Index");
            }
            string query = "SELECT b.Building_Name as Building_Name, r.Building_ID as Building_ID, r.Park_ID as Park_ID, r.Room_ID as Room_ID, r.Capacity as Capacity, r.Type_ID as Type_ID from timetable_building b, timetable_room r WHERE b.Building_ID = r.Building_ID AND r.Building_ID = '" + buildingID + "' AND r.Room_ID = '" + roomID + "'";
            var viewModel = db.Database.SqlQuery<WebApplication1.Models.RoomBuildingViewModel>(query).First();
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // GET: /Room/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Park_ID,Building_ID,Room_ID,Capacity,Type_ID")] timetable_room timetable_room)
        {
            if (ModelState.IsValid)
            {
                db.timetable_room.Add(timetable_room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_room);
        }

        // GET: /Room/Edit/5
        public ActionResult Edit(string buildingID, string roomID)
        {
            if (roomID == null || buildingID == null)
            {
                return RedirectToAction("Index");
            }
            timetable_room timetable_room = db.timetable_room.Where(bi => bi.Building_ID == buildingID).Where(ri => ri.Room_ID == roomID).First();
            if (timetable_room == null)
            {
                return HttpNotFound();
            }
            return View(timetable_room);
        }

        // POST: /Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Park_ID,Building_ID,Room_ID,Capacity,Type_ID")] timetable_room timetable_room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable_room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_room);
        }

        // GET: /Room/Delete/5
        public ActionResult Delete(string buildingID, string roomID)
        {
            if (roomID == null || buildingID == null)
            {
                return RedirectToAction("Index");
            }
            timetable_room timetable_room = db.timetable_room.Where(bi => bi.Building_ID == buildingID).Where(ri => ri.Room_ID == roomID).First();
            if (timetable_room == null)
            {
                return HttpNotFound();
            }
            return View(timetable_room);
        }

        // POST: /Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string buildingID, string roomID)
        {
            timetable_room timetable_room = db.timetable_room.Where(bi => bi.Building_ID == buildingID).Where(ri => ri.Room_ID == roomID).First();
            db.timetable_room.Remove(timetable_room);
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

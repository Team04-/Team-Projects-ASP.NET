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
    public class RoomController : Controller
    {
        private team04Entities db = new team04Entities();

        //
        // GET: /Room/
        [Authorize]
        public ActionResult Index()
        {
            string query = "SELECT b.Building_Name as Building_Name, r.Building_ID as Building_ID, b.Park_ID as Park_ID, r.Room_ID as Room_ID, r.Capacity as Capacity, r.Type_ID as Type_ID from timetable_building b, timetable_room r WHERE b.Building_ID = r.Building_ID";
            var viewModel = db.Database.SqlQuery<TeamProjects.Models.RoomBuildingViewModel>(query);
            return View(viewModel.ToList());
        }

        //
        // GET: /Room/Details/5
        [Authorize]
        public ActionResult Details(string buildingID = null, string roomID = null)
        {
            if (roomID == null || buildingID == null)
            {
                return RedirectToAction("Index");
            }
            string query = "SELECT b.Building_Name as Building_Name, r.Building_ID as Building_ID, b.Park_ID as Park_ID, r.Room_ID as Room_ID, r.Capacity as Capacity, r.Type_ID as Type_ID from timetable_building b, timetable_room r WHERE b.Building_ID = r.Building_ID AND r.Building_ID = '" + buildingID + "' AND r.Room_ID = '" + roomID + "'";
            var viewModel = db.Database.SqlQuery<TeamProjects.Models.RoomBuildingViewModel>(query).First();
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        //
        // GET: /Room/Create
        [Authorize]
        public ActionResult Create()
        {
            List<timetable_building> buildingList = db.timetable_building.ToList();
            List<string> buildingCodes = new List<string>();
            List<string> buildingNames = new List<string>();
            foreach (timetable_building building in buildingList)
            {
                buildingCodes.Add(building.Building_ID);
                buildingNames.Add(building.Building_Name);
            }
            ViewBag.buildingCodes = buildingCodes;
            ViewBag.buildingNames = buildingNames;
            return View();
        }

        //
        // POST: /Room/Create

        [HttpPost]
        public ActionResult Create(timetable_room timetable_room)
        {
            if (ModelState.IsValid)
            {
                db.timetable_room.Add(timetable_room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_room);
        }

        //
        // GET: /Room/Edit/5
        [Authorize]
        public ActionResult Edit(string buildingID = null, string roomID = null)
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

        //
        // POST: /Room/Edit/5

        [HttpPost]
        public ActionResult Edit(timetable_room timetable_room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable_room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_room);
        }

        //
        // GET: /Room/Delete/5
        [Authorize]
        public ActionResult Delete(string buildingID = null, string roomID = null)
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

        //
        // POST: /Room/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string buildingID, string roomID)
        {
            timetable_room timetable_room = db.timetable_room.Where(bi => bi.Building_ID == buildingID).Where(ri => ri.Room_ID == roomID).First();
            db.timetable_room.Remove(timetable_room);
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
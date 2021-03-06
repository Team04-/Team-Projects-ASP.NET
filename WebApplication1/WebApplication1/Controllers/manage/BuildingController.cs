﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team_Projects.Models;

namespace Team_Projects.Controllers.manage
{
    public class BuildingController : Controller
    {
        private team04Entities db = new team04Entities();

        //
        // GET: /Building/

        public ActionResult Index()
        {
            return View(db.timetable_building.ToList());
        }

        //
        // GET: /Building/Details/5

        public ActionResult Details(string id = null)
        {
            timetable_building timetable_building = db.timetable_building.Single(t => t.Building_ID == id);
            if (timetable_building == null)
            {
                return HttpNotFound();
            }
            return View(timetable_building);
        }

        //
        // GET: /Building/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Building/Create

        [HttpPost]
        public ActionResult Create(timetable_building timetable_building)
        {
            if (ModelState.IsValid)
            {
                db.timetable_building.AddObject(timetable_building);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_building);
        }

        //
        // GET: /Building/Edit/5

        public ActionResult Edit(string id = null)
        {
            timetable_building timetable_building = db.timetable_building.Single(t => t.Building_ID == id);
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
                db.timetable_building.Attach(timetable_building);
                db.ObjectStateManager.ChangeObjectState(timetable_building, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_building);
        }

        //
        // GET: /Building/Delete/5

        public ActionResult Delete(string id = null)
        {
            timetable_building timetable_building = db.timetable_building.Single(t => t.Building_ID == id);
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
            timetable_building timetable_building = db.timetable_building.Single(t => t.Building_ID == id);
            db.timetable_building.DeleteObject(timetable_building);
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
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ModuleController : Controller
    {
        private TestDBEntities db = new TestDBEntities();

        // GET: /Module/
        [Authorize]
        public ActionResult Index()
        {
            return View(db.timetable_module.Where(dc => dc.Department_Code == User.Identity.Name));
        }

        // GET: /Module/Details/5
        [Authorize]
        public ActionResult Details(string moduleCode)
        {
            if (moduleCode == null)
            {
                return RedirectToAction("Index");
            }
            timetable_module timetable_module = db.timetable_module.Where(dc => dc.Department_Code == User.Identity.Name).Where(mc => mc.Module_Code == moduleCode).First();
            if (timetable_module == null)
            {
                return HttpNotFound();
            }
            return View(timetable_module);
        }

        // GET: /Module/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Department_Code,Part_Code,Module_Code,Module_Title,Active")] timetable_module timetable_module)
        {
            if (ModelState.IsValid)
            {
                db.timetable_module.Add(timetable_module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_module);
        }

        // GET: /Module/Edit/5
        [Authorize]
        public ActionResult Edit(string moduleCode)
        {
            if (moduleCode == null)
            {
                return RedirectToAction("Index");
            }
            timetable_module timetable_module = db.timetable_module.Where(dc => dc.Department_Code == User.Identity.Name).Where(mc => mc.Module_Code == moduleCode).First();
            if (timetable_module == null)
            {
                return HttpNotFound();
            }
            return View(timetable_module);
        }

        // POST: /Module/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Department_Code,Part_Code,Module_Code,Module_Title,Active")] timetable_module timetable_module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable_module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_module);
        }

        // GET: /Module/Delete/5
        [Authorize]
        public ActionResult Delete(string moduleCode)
        {
            if (moduleCode == null)
            {
                return RedirectToAction("Index");
            }
            timetable_module timetable_module = db.timetable_module.Where(dc => dc.Department_Code == User.Identity.Name).Where(mc => mc.Module_Code == moduleCode).First();
            if (timetable_module == null)
            {
                return HttpNotFound();
            }
            return View(timetable_module);
        }

        // POST: /Module/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string moduleCode)
        {
            timetable_module timetable_module = db.timetable_module.Where(dc => dc.Department_Code == User.Identity.Name).Where(mc => mc.Module_Code == moduleCode).First();
            db.timetable_module.Remove(timetable_module);
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

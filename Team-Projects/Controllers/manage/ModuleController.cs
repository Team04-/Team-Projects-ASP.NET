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
    public class ModuleController : Controller
    {
        private team04Entities db = new team04Entities();

        //
        // GET: /Module/

        public ActionResult Index()
        {
            return View(db.timetable_module.ToList());
        }

        //
        // GET: /Module/Details/5

        public ActionResult Details(string id = null)
        {
            timetable_module timetable_module = db.timetable_module.Single(t => t.Department_Code == id);
            if (timetable_module == null)
            {
                return HttpNotFound();
            }
            return View(timetable_module);
        }

        //
        // GET: /Module/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Module/Create

        [HttpPost]
        public ActionResult Create(timetable_module timetable_module)
        {
            if (ModelState.IsValid)
            {
                db.timetable_module.AddObject(timetable_module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_module);
        }

        //
        // GET: /Module/Edit/5

        public ActionResult Edit(string id = null)
        {
            timetable_module timetable_module = db.timetable_module.Single(t => t.Department_Code == id);
            if (timetable_module == null)
            {
                return HttpNotFound();
            }
            return View(timetable_module);
        }

        //
        // POST: /Module/Edit/5

        [HttpPost]
        public ActionResult Edit(timetable_module timetable_module)
        {
            if (ModelState.IsValid)
            {
                db.timetable_module.Attach(timetable_module);
                db.ObjectStateManager.ChangeObjectState(timetable_module, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_module);
        }

        //
        // GET: /Module/Delete/5

        public ActionResult Delete(string id = null)
        {
            timetable_module timetable_module = db.timetable_module.Single(t => t.Department_Code == id);
            if (timetable_module == null)
            {
                return HttpNotFound();
            }
            return View(timetable_module);
        }

        //
        // POST: /Module/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            timetable_module timetable_module = db.timetable_module.Single(t => t.Department_Code == id);
            db.timetable_module.DeleteObject(timetable_module);
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
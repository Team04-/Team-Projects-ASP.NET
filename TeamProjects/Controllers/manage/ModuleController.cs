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
    public class ModuleController : Controller
    {
        private team04Entities db = new team04Entities();

        //
        // GET: /Module/

        public ActionResult Index()
        {
            return View(db.timetable_module.Where(dc => dc.Department_Code == User.Identity.Name));
        }

        //
        // GET: /Module/Details/5

        public ActionResult Details(string moduleCode = null)
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
                db.timetable_module.Add(timetable_module);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_module);
        }

        //
        // GET: /Module/Edit/5

        public ActionResult Edit(string moduleCode = null)
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

        //
        // POST: /Module/Edit/5

        [HttpPost]
        public ActionResult Edit(timetable_module timetable_module)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable_module).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_module);
        }

        //
        // GET: /Module/Delete/5

        public ActionResult Delete(string moduleCode = null)
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

        //
        // POST: /Module/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string moduleCode)
        {
            timetable_module timetable_module = db.timetable_module.Where(dc => dc.Department_Code == User.Identity.Name).Where(mc => mc.Module_Code == moduleCode).First();
            db.timetable_module.Remove(timetable_module);
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
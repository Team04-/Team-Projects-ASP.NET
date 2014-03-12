using System;
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
        public ActionResult Index()
        {
            return View(db.timetable_module.ToList());
        }

        // GET: /Module/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable_module timetable_module = db.timetable_module.Find(id);
            if (timetable_module == null)
            {
                return HttpNotFound();
            }
            return View(timetable_module);
        }

        // GET: /Module/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Module/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable_module timetable_module = db.timetable_module.Find(id);
            if (timetable_module == null)
            {
                return HttpNotFound();
            }
            return View(timetable_module);
        }

        // POST: /Module/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            timetable_module timetable_module = db.timetable_module.Find(id);
            if (timetable_module == null)
            {
                return HttpNotFound();
            }
            return View(timetable_module);
        }

        // POST: /Module/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            timetable_module timetable_module = db.timetable_module.Find(id);
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

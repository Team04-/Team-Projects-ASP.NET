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
    public class RoundController : Controller
    {
        private team04Entities db = new team04Entities();

        //
        // GET: /Round/

        public ActionResult Index()
        {
            return View(db.timetable_round.ToList());
        }

        //
        // GET: /Round/Details/5

        public ActionResult Details(short id = 0)
        {
            timetable_round timetable_round = db.timetable_round.Find(id);
            if (timetable_round == null)
            {
                return HttpNotFound();
            }
            return View(timetable_round);
        }

        //
        // GET: /Round/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Round/Create

        [HttpPost]
        public ActionResult Create(timetable_round timetable_round)
        {
            if (ModelState.IsValid)
            {
                db.timetable_round.Add(timetable_round);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(timetable_round);
        }

        //
        // GET: /Round/Edit/5

        public ActionResult Edit(short id = 0)
        {
            timetable_round timetable_round = db.timetable_round.Find(id);
            if (timetable_round == null)
            {
                return HttpNotFound();
            }
            return View(timetable_round);
        }

        //
        // POST: /Round/Edit/5

        [HttpPost]
        public ActionResult Edit(timetable_round timetable_round)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable_round).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_round);
        }

        //
        // GET: /Round/Delete/5

        public ActionResult Delete(short id = 0)
        {
            timetable_round timetable_round = db.timetable_round.Find(id);
            if (timetable_round == null)
            {
                return HttpNotFound();
            }
            return View(timetable_round);
        }

        //
        // POST: /Round/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(short id)
        {
            timetable_round timetable_round = db.timetable_round.Find(id);
            db.timetable_round.Remove(timetable_round);
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
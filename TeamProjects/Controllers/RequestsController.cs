using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProjects.Models;

namespace TeamProjects.Controllers  
{
    public class RequestsController : Controller
    {
        private team04Entities db = new team04Entities();

        //
        // GET: /Requests/
        [Authorize]
        public ActionResult Index()
        {
            var check = db.timetable_round.Where(r => r.Round_Status == "Current").First();
            if (!(check is timetable_round))
            {
                check = db.timetable_round.Last();
            }
            ViewBag.currentRoundCode = check.Round_Code;
            return View(db.timetable_request.ToList());
        }

        //
        // GET: /Requests/Details/5
        [Authorize]
        public ActionResult Details(short id = 0)
        {
            timetable_request timetable_request = db.timetable_request.Find(id);
            if (timetable_request == null)
            {
                return HttpNotFound();
            }
            return View(timetable_request);
        }

        //
        // GET: /Requests/Create
        [Authorize]
        public void Create()
        {
            Response.Redirect("~/Request/Create");
        }

        //
        // POST: /Requests/Create
        [HttpPost]
        public void Create(timetable_request timetable_request)
        {
            Response.Redirect("~/Request/Create");
        }

        //
        // GET: /Requests/Edit/5
        [Authorize]
        public ActionResult Edit(short id = 0)
        {
            timetable_request timetable_request = db.timetable_request.Find(id);
            if (timetable_request == null)
            {
                return HttpNotFound();
            }
            return View(timetable_request);
        }

        //
        // POST: /Requests/Edit/5
        [HttpPost]
        public ActionResult Edit(timetable_request timetable_request)
        {
            if (ModelState.IsValid)
            {
                db.Entry(timetable_request).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(timetable_request);
        }

        //
        // GET: /Requests/Delete/5
        [Authorize]
        public ActionResult Delete(short id = 0)
        {
            timetable_request timetable_request = db.timetable_request.Find(id);
            if (timetable_request == null)
            {
                return HttpNotFound();
            }
            return View(timetable_request);
        }

        //
        // POST: /Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(short id)
        {
            timetable_request timetable_request = db.timetable_request.Find(id);
            db.timetable_request.Remove(timetable_request);
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
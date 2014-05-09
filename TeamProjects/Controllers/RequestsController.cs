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
        private Models.RequestListModel getFullRequest(Models.timetable_request request)
        {
            Models.RequestListModel newRequest = new Models.RequestListModel();
            newRequest.Request_ID = request.Request_ID;
            newRequest.Module_Code = request.Module_Code;
            newRequest.Custom_Comments = request.Custom_Comments;
            newRequest.Round = request.Current_Round;
            newRequest.Park = request.Park_ID;
            newRequest.Priority = request.Priority;
            newRequest.Number_Rooms = request.Number_Rooms;
            newRequest.Number_Students = request.Number_Students;
            newRequest.Start_Period = request.Start_Time;
            string weekDay = "";
            switch (int.Parse(request.Day_ID.ToString()))
            {
                case 1:
                    weekDay = "Mon";
                    break;
                case 2:
                    weekDay = "Tues";
                    break;
                case 3:
                    weekDay = "Wed";
                    break;
                case 4:
                    weekDay = "Thur";
                    break;
                case 5:
                    weekDay = "Fri";
                    break;
                case 6:
                    weekDay = "Sat";
                    break;
                case 7:
                    weekDay = "Sun";
                    break;
                default:
                    weekDay = "Unknown";
                    break;
            }
            newRequest.Day = weekDay;
            string status = "";
            switch (int.Parse(request.Request_Status.ToString()))
            {
                case 1:
                    status = "Pending";
                    break;
                case 2:
                    status = "Accepted";
                    break;
                case 3:
                    status = "Failed";
                    break;
                case 4:
                    status = "Altered";
                    break;
                default:
                    status = "Unknown";
                    break;
            }
            newRequest.Status = status;

            newRequest.Module_Title = db.timetable_module.Where(m => m.Module_Code == request.Module_Code).First().Module_Title;
            newRequest.Has_Comments = (request.Custom_Comments == null) ? (false) : (!(request.Custom_Comments.Trim() == ""));
            newRequest.End_Period = ((request.Start_Time + request.Duration) - 1);

            newRequest.Time_String = GetTimeString(newRequest.Start_Period) + " - " + GetTimeString(newRequest.End_Period + 1);

            List<timetable_request_week> weekList = db.timetable_request_week.Where(rw => rw.Request_ID == request.Request_ID).ToList();
            bool[] weekListArray = new bool[15];
            for (int i = 0; i < 15; i++)
            {
                weekListArray[i] = false;
            }
            foreach (timetable_request_week week in weekList)
            {
                weekListArray[week.Week-1] = true;
            }
            newRequest.Weeks = weekListArray;
            List<timetable_request_room_allocation> roomList = db.timetable_request_room_allocation.Where(ra => ra.Request_ID == request.Request_ID).ToList();
            string[] roomArray = new string[roomList.Count];
            int roomCounter = 0;
            foreach (timetable_request_room_allocation room in roomList)
            {
                roomArray[roomCounter] = room.Building_ID.ToString() + room.Room_ID.ToString();
                roomCounter++;
            }
            newRequest.Rooms = roomArray;
            return newRequest;
        }
        private string GetTimeString(int period)
        {
            string time = "";
            switch (period)
            {
                case 1:
                    time = "09:00";
                    break;
                case 2:
                    time = "10:00";
                    break;
                case 3:
                    time = "11:00";
                    break;
                case 4:
                    time = "12:00";
                    break;
                case 5:
                    time = "13:00";
                    break;
                case 6:
                    time = "14:00";
                    break;
                case 7:
                    time = "15:00";
                    break;
                case 8:
                    time = "16:00";
                    break;
                case 9:
                    time = "17:00";
                    break;
                case 10:
                    time = "18:00";
                    break;
            }
            return time;
        }
        //
        // GET: /Requests/
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        
        //
        //GET: /Requests/List
        [Authorize]
        public ActionResult List()
        {
            List<Models.RequestListModel> RequestList = new List<Models.RequestListModel>();
            List<timetable_request> Requests = db.timetable_request.OrderBy(r => r.Current_Round).ToList();
            foreach(timetable_request request in Requests)
            {
                RequestList.Add(getFullRequest(request));
            }
            var check = db.timetable_round.Where(r => r.Round_Status == "Current").First();
            if (!(check is timetable_round))
            {
                check = db.timetable_round.Last();
            }
            ViewBag.currentRoundCode = check.Round_Code;
            return View(RequestList.OrderBy(r => r.Request_ID).ThenByDescending(r => r.Round).ToList());
        }

        //
        // GET: /Requests/Details/5
        [Authorize]
        public ActionResult Details(short id = 0)
        {
            return RedirectToAction("List");
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
        public ActionResult Edit(short id)
        {
            timetable_request request = db.timetable_request.Where(r => r.Request_ID == id).FirstOrDefault();
            if (id == null)
            {
                return HttpNotFound();
            }
			ViewBag.id = id;
            return View();
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
                return RedirectToAction("List");
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
            var check = db.timetable_round.Where(r => r.Round_Status == "Current").First();
            if (!(check is timetable_round))
            {
                check = db.timetable_round.Last();
            }
            ViewBag.currentRoundCode = check.Round_Code;
            Models.RequestListModel request = getFullRequest(timetable_request);
            return View(request);
        }

        //
        // POST: /Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(short id)
        {
            //Get rows to remove
            timetable_request timetable_request = db.timetable_request.Find(id);
            IQueryable<timetable_request_facility> facilities = db.timetable_request_facility.Where(rf => rf.Request_ID == id);
            IQueryable<timetable_request_room_allocation> allocations = db.timetable_request_room_allocation.Where(ra => ra.Request_ID == id);
            IQueryable<timetable_request_week> weeks = db.timetable_request_week.Where(rw => rw.Request_ID == id);
            //Remove rows from tables
            db.timetable_request.Remove(timetable_request);
            foreach (timetable_request_facility facility in facilities)
            {
                db.timetable_request_facility.Remove(facility);
            }
            foreach (timetable_request_room_allocation allocation in allocations)
            {
                db.timetable_request_room_allocation.Remove(allocation);
            }
            foreach (timetable_request_week week in weeks)
            {
                db.timetable_request_week.Remove(week);
            }
            //Save db with rows removed
            db.SaveChanges();
            return RedirectToAction("List");
        }

        //
        // GET: /Requests/Timetable
        [HttpGet]
        public ActionResult Timetable()
        {
            ViewBag.RoundCodes = db.timetable_round.Where(r => r.Round_Status == "Current" || r.Round_Status == "Active").Select(r => r.Round_Code).ToArray();
            var roundStarts = db.timetable_round.Where(r => r.Round_Status == "Current" || r.Round_Status == "Active").Select(r => r.Start_Date).ToArray();
            var roundEnds = db.timetable_round.Where(r => r.Round_Status == "Current" || r.Round_Status == "Active").Select(r => r.End_Date).ToArray();
            string[] newRoundStarts = new string[roundStarts.Length];
            string[] newRoundEnds = new string[roundEnds.Length];
            for (int i = 0; i < roundStarts.Length;i++ )
            {
                newRoundStarts[i] = roundStarts[i].ToString().Split()[0];
                newRoundEnds[i] = roundEnds[i].ToString().Split()[0];
            }
            ViewBag.RoundStarts = newRoundStarts;
            ViewBag.RoundEnds = newRoundEnds;
			ViewBag.RoundSemester = new SelectList(db.timetable_round, "Round_Code", "RoundSemesterString");
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
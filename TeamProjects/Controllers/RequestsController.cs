using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
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
			ViewBag.Number_Rooms = new SelectList(new[] { "1", "2", "3" });
			ViewBag.Start_Time = new SelectList(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
			ViewBag.Duration = new SelectList(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
			ViewBag.Department_Code = new SelectList(db.timetable_department, "Department_Code", "Department_Name");
			//ViewBag.Part_Code = new SelectList(db.timetable_request, "Part_Code", "Part_Code");
			ViewBag.Module_Code = new SelectList(db.timetable_module, "Module_Code", "Module_Title");
			ViewBag.Day_ID = new SelectList(db.timetable_day, "Day_ID", "Day_Name");
			ViewBag.Park_ID = new SelectList(db.timetable_park, "Park_ID", "Park_Name");
			ViewBag.Building_ID = new SelectList(db.timetable_building, "Building_ID", "Building_Name");
			ViewBag.Room_ID = new SelectList(db.timetable_room, "Room_ID", "Room_ID");
			ViewBag.Facility_ID = new SelectList(db.timetable_facility, "Facility_ID", "Facility_Name");
			ViewBag.Room_Type = new SelectList(db.timetable_room_type, "Type_ID", "Type_Name");

			ViewBag.id = id;

            return View();
        }

        //
        // POST: /Requests/Edit/5
        [HttpPost]
		public ActionResult Edit(RequestViewModel requestView)
        {
			var requID = Convert.ToInt16(requestView.Request_ID);
			
			var queryReq = (from s in db.timetable_request where s.Request_ID == requID select s).FirstOrDefault();

			db.timetable_request.Attach(queryReq);

				//Request_ID = requID,
				queryReq.Department_Code = User.Identity.Name; //requestView.Department_Code, // Get from logged in user
				//Part_Code = requestView.Part_Code.ToString(), // Dropdown
				queryReq.Module_Code = requestView.Module_Code;
				queryReq.Day_ID = (byte)requestView.Day_ID;
				queryReq.Park_ID = requestView.Park_ID;
				queryReq.Start_Time = (byte)requestView.Start_Time;
				queryReq.Duration = (byte)requestView.Duration;
				queryReq.Number_Students = requestView.Number_Students;
				queryReq.Number_Rooms = (byte)requestView.Number_Rooms;
				queryReq.Priority = requestView.Priority;
				queryReq.Custom_Comments = requestView.Custom_Comments;
				queryReq.Current_Round = db.timetable_round.Where(r => r.Round_Status == "Current").First().Round_Code;
				queryReq.Request_Status = 1;

			db.SaveChanges();

			//timetable_request timetable_request = new timetable_request()
			//{
			//	Request_ID = requID,
			//	Department_Code = User.Identity.Name, //requestView.Department_Code, // Get from logged in user
			//	//Part_Code = requestView.Part_Code.ToString(), // Dropdown
			//	Module_Code = requestView.Module_Code,
			//	Day_ID = (byte)requestView.Day_ID,
			//	Park_ID = requestView.Park_ID,
			//	Start_Time = (byte)requestView.Start_Time,
			//	Duration = (byte)requestView.Duration,
			//	Number_Students = requestView.Number_Students,
			//	Number_Rooms = (byte)requestView.Number_Rooms,
			//	Priority = requestView.Priority,
			//	Custom_Comments = requestView.Custom_Comments,
			//	Current_Round = db.timetable_round.Where(r => r.Round_Status == "Current").First().Round_Code,
			//	Request_Status = 1
			//};

			IQueryable<timetable_request_facility> facilities = db.timetable_request_facility.Where(rf => rf.Request_ID == requID);
			IQueryable<timetable_request_room_allocation> allocations = db.timetable_request_room_allocation.Where(ra => ra.Request_ID == requID);
			IQueryable<timetable_request_week> weeks = db.timetable_request_week.Where(rw => rw.Request_ID == requID);
			//Remove rows from tables
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


			JavaScriptSerializer serializer = new JavaScriptSerializer();

			// [Redundancy...
			if (requestView.Fac_JSON != null && requestView.Fac_JSON != "[\"\"]")
			{

				var facs = serializer.Deserialize<byte[]>(requestView.Fac_JSON);

				foreach (var item in facs)
				{
					timetable_request_facility timetable_request_facility = new timetable_request_facility()
					{
						Request_ID = requID,
						Facility_ID = item,
						Quantity = 1,
					};

					db.timetable_request_facility.Add(timetable_request_facility);
					db.SaveChanges();
				}
			}
			// ...]
			if (requestView.Room_Pref_JSON != null)
			{
				var roomPrefsArray = (requestView.Room_Pref_JSON).Split(',');

				for (var i = 1; i < roomPrefsArray.Length - 1; i += 2)
				{
					timetable_request_room_allocation timetable_request_room_allocation = new timetable_request_room_allocation()
					{
						Request_ID = requID,
						Building_ID = roomPrefsArray[i],
						Room_ID = roomPrefsArray[i + 1],
					};

					db.timetable_request_room_allocation.Add(timetable_request_room_allocation);
					db.SaveChanges();
				}

			}

			byte weekCount = 1;

			timetable_request_week timetable_request_week;

			// TO DO TODO: From here....
			bool[] weekList = new bool[] { requestView.WeekOne, requestView.WeekTwo, requestView.WeekThree, requestView.WeekFour, requestView.WeekFive, requestView.WeekSix, requestView.WeekSeven, requestView.WeekEight, requestView.WeekNine, requestView.WeekTen, requestView.WeekEleven, requestView.WeekTwelve, requestView.WeekThirteen, requestView.WeekFourteen, requestView.WeekFifteen };

			for (int i = 0; i < weekList.Length; i++)
			{
				if (weekList[i] == true)
				{
					timetable_request_week = new timetable_request_week()
					{
						Request_ID = requID,
						Week = Convert.ToByte(i + 1),
						//WeekReqID = (Int16)((db.timetable_request_week.ToList().Last().WeekReqID) + 1),
					};

					db.timetable_request_week.Add(timetable_request_week);
					db.SaveChanges();
				}
				weekCount++;
			}

			// TO DO TODO: ...To here.

			return RedirectToAction("List", "Requests");
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
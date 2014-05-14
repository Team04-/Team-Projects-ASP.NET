using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Mvc;
using TeamProjects.Models;

namespace TeamProjects.Controllers.manage
{
    public class RequestController : Controller
    {
        private team04Entities db = new team04Entities();


        //
        // GET: /Request/
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
        // GET: /Request/Details/5
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

        // GET: /Request/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Number_Rooms = new SelectList(new[] { "1", "2", "3" });
            ViewBag.Start_Time = new SelectList(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
            ViewBag.Duration = new SelectList(new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
            ViewBag.Department_Code = new SelectList(db.timetable_department, "Department_Code", "Department_Name");
            //ViewBag.Part_Code = new SelectList(db.timetable_request, "Part_Code", "Part_Code");
            ViewBag.Module_Code = new SelectList(db.timetable_module, "Module_Code", "Module_Title");
            List<timetable_module> modules = db.timetable_module.Where(m => m.Department_Code==User.Identity.Name).OrderBy(m => m.Module_Code).ToList();
            List<string> codes = new List<string>();
            List<string> titles = new List<string>();
            for(int i=0;i<modules.Count;i++){
                codes.Add(modules[i].Module_Code);
                titles.Add(modules[i].Module_Title);
            }
            ViewBag.codes = codes;
            ViewBag.titles = titles;

			ViewBag.Current_Round = new SelectList(db.timetable_round.Where(r => r.Round_Status == "Current" || r.Round_Status == "Active"), "Round_Code", "Round_Code");
            ViewBag.Day_ID = new SelectList(db.timetable_day, "Day_ID", "Day_Name");
            ViewBag.Park_ID = new SelectList(db.timetable_park, "Park_ID", "Park_Name");
			ViewBag.Building_ID = new SelectList(db.timetable_building, "Building_ID", "Building_Name");
			ViewBag.Room_ID = new SelectList(db.timetable_room, "Room_ID", "Room_ID");
			ViewBag.Facility_ID = new SelectList(db.timetable_facility, "Facility_ID", "Facility_Name");
			ViewBag.Room_Type = new SelectList(db.timetable_room_type, "Type_ID", "Type_Name");

            return View();
        }

		public ActionResult GetRoomPrefCollection(string[][] roomPrefColl)
		{
			var cr = new JsonResult();
			cr.Data = roomPrefColl;
			return cr;			
		}

        // POST: /Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(RequestViewModel requestView)
        {
			timetable_request timetable_request = new timetable_request()
			{
				//Request_ID = newReqID,
				Department_Code = User.Identity.Name, //requestView.Department_Code, // Get from logged in user
				//Part_Code = requestView.Part_Code.ToString(), // Dropdown
				Module_Code = requestView.Module_Code,
				Day_ID = (byte) requestView.Day_ID,
				Park_ID = requestView.Park_ID,
				Start_Time = (byte) requestView.Start_Time,
				Duration = (byte) requestView.Duration,
				Number_Students = requestView.Number_Students,
				Number_Rooms = (byte) requestView.Number_Rooms,
				Priority = requestView.Priority,
				Custom_Comments = requestView.Custom_Comments,
				Current_Round = requestView.Current_Round, //db.timetable_round.Where(r => r.Round_Status == "Current").First().Round_Code,
				Request_Status = 1
			};

			db.timetable_request.Add(timetable_request);
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
                        Request_ID = ((Int16)((from s in db.timetable_request select s.Request_ID).Max())),
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
						Request_ID = ((Int16)((from s in db.timetable_request select s.Request_ID).Max())),
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
            bool[] weekList = new bool[] {requestView.WeekOne, requestView.WeekTwo, requestView.WeekThree, requestView.WeekFour, requestView.WeekFive, requestView.WeekSix, requestView.WeekSeven, requestView.WeekEight, requestView.WeekNine, requestView.WeekTen, requestView.WeekEleven, requestView.WeekTwelve, requestView.WeekThirteen, requestView.WeekFourteen, requestView.WeekFifteen};

            for (int i = 0; i < weekList.Length;i++)
            {
                if (weekList[i] == true)
                {
                    timetable_request_week = new timetable_request_week()
                    {
						Request_ID = ((Int16)((from s in db.timetable_request select s.Request_ID).Max())),
                        Week = Convert.ToByte(i+1),
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
    }
}

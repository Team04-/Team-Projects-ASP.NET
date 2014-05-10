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
            ViewBag.Day_ID = new SelectList(db.timetable_day, "Day_ID", "Day_Name");
            ViewBag.Park_ID = new SelectList(db.timetable_park, "Park_ID", "Park_Name");
			ViewBag.Building_ID = new SelectList(db.timetable_building, "Building_ID", "Building_Name");
			ViewBag.Room_ID = new SelectList(db.timetable_room, "Room_ID", "Room_ID");
			ViewBag.Facility_ID = new SelectList(db.timetable_facility, "Facility_ID", "Facility_Name");
			ViewBag.Room_Type = new SelectList(db.timetable_room_type, "Type_ID", "Type_Name");

            return View();
        }

		//public bool GetRoomPrefCollection(string[][] roomPrefColl)
		public ActionResult GetRoomPrefCollection(string[][] roomPrefColl)
		{
			var cr = new JsonResult();
			cr.Data = roomPrefColl;
			return cr;

			//if (roomPrefColl != null)
			//{
			//	return true;
			//}

			//else {
			//	return false;
			//}
			
		}

        // POST: /Request/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(RequestViewModel requestView)
		//public ActionResult Create([Bind(Include="Request_ID,Department_Code,Part_Code,Module_Code,Day_ID,Start_Time,Duration,Number_Students,Number_Rooms,Priority,Room_Type,Park_ID,Custom_Comments,Current_Year,Current_Semester,Current_Round,Request_Status")] RequestViewModel requestView)
        {
            // Make new timetable_request object, add requestView ("master data") Current_Round value.
            // Do this for all tables which need subsets of data from requestView.

			//string query = "SELECT dept.Department_Code as Department_Code, mod.Part_Code as Part_Code, mod.Module_Code as Module_Code, roomt.Type_Name as Room_Type, park.Park_ID as Park_ID from timetable_department dept, timetable_request mod, timetable_room_type roomt, timetable_park park";
			//var viewModel = db.Database.SqlQuery<TeamProjects.Models.RequestViewModel>(query);
			//return View(viewModel.ToList());

			

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
                //TODO change so that it uses the departments selected active round
				Current_Round = db.timetable_round.Where(r => r.Round_Status == "Current").First().Round_Code,
				Request_Status = 1
			};

			db.timetable_request.Add(timetable_request);
			db.SaveChanges();

			JavaScriptSerializer serializer = new JavaScriptSerializer();

			var newReqID = (Int16)((db.timetable_request.ToList().Last().Request_ID) + 1);

            if (requestView.Fac_JSON != null)
            {

                var facs = serializer.Deserialize<byte[]>(requestView.Fac_JSON);

                foreach (var item in facs)
                {
                    timetable_request_facility timetable_request_facility = new timetable_request_facility()
                    {
                        Request_ID = newReqID,
                        Facility_ID = item,
                        Quantity = 1,
                    };

                    db.timetable_request_facility.Add(timetable_request_facility);
                    db.SaveChanges();
                }
            }

            if (requestView.Room_Pref_JSON != null)
            {

                var roomPrefs = serializer.Deserialize<RoomPref[]>(requestView.Room_Pref_JSON);

                foreach (var item in roomPrefs)
                {
                    if (item.Building.ToString() == "null" || item.Building.ToString() == "N/A" || item.Room.ToString() == "0" || item.Room.ToString() == "N/A")
                    {
                    }
					else
					{
						timetable_request_room_allocation timetable_request_room_allocation = new timetable_request_room_allocation()
						{
							Request_ID = newReqID,
							Building_ID = item.Building.ToString(),
							Room_ID = item.Room.ToString(),
						};

						db.timetable_request_room_allocation.Add(timetable_request_room_allocation);
						db.SaveChanges();
					}
                }
            }

			//timetable_request_week timetable_request_week = new timetable_request_week()
			//{
			//	Request_ID = newReqID,
				//Week = Convert.ToByte(requestView.WeekOne),
			//};

           // timetable_request_week timetable_request_week = new timetable_request_week();

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
                        Request_ID = newReqID,
                        Week = Convert.ToByte(i),
                        //WeekReqID = (Int16)((db.timetable_request_week.ToList().Last().WeekReqID) + 1),
                    };

                    db.timetable_request_week.Add(timetable_request_week);
                    db.SaveChanges();
                }
                weekCount++;
            }

			// TO DO TODO: ...To here.

			//return RedirectToAction("List");
			return RedirectToAction("List", "Requests");
        }


        //
        // GET: /Request/Edit/5
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
        // POST: /Request/Edit/5

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
        // GET: /Request/Delete/5
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
        // POST: /Request/Delete/5

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

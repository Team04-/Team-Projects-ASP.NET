using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeamProjects.Models;

namespace TeamProjects.Controllers
{
    public class HomeController : Controller
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
        [Authorize]
        public ActionResult Index()
        {
            team04Entities db = new team04Entities();
            int requestCount = db.timetable_request.Count();
            int toSkip = (requestCount > 5) ? requestCount - 5 : 0;
            List<RequestListModel> RecentlyAdded = new List<RequestListModel>();
            List<RequestListModel> Attention = new List<RequestListModel>();
            foreach (timetable_request request in db.timetable_request.Where(r => r.Department_Code == User.Identity.Name).OrderBy(r => r.Request_ID).Skip(toSkip).OrderByDescending(r => r.Request_ID))
            {
                RecentlyAdded.Add(getFullRequest(request));
            }
            foreach (timetable_request request in db.timetable_request.Where(r =>r.Department_Code == User.Identity.Name).Where(r => r.Request_Status == 3 || r.Request_Status == 4).OrderByDescending(r => r.Request_ID))
            {
                Attention.Add(getFullRequest(request));
            }
            ViewBag.RecentlyAdded = RecentlyAdded;
            ViewBag.Attention = Attention;
            ViewBag.RoundInfo = db.timetable_round.Where(r => r.Round_Status == "Current").FirstOrDefault();
            return View();
        }
    }
}

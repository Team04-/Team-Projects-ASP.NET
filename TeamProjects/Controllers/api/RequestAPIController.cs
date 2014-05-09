using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TeamProjects.Models;

namespace TeamProjects.Controllers.api
{
    public class RequestAPIController : ApiController
    {
        private team04Entities db = new team04Entities();

        // GET api/RequestAPIController
        public IEnumerable<RequestListModel> Gettimetable_requests(int Round, int Week)
        {
            List<Models.RequestListModel> RequestList = new List<Models.RequestListModel>();
			List<timetable_request> Requests;

			if (Round == 0) { Requests = db.timetable_request.ToList(); }
			else { Requests = db.timetable_request.Where(m => m.Current_Round == Round).ToList(); }
            //Requests = from m in db.timetable_room join Requests where m.Type_ID == selID select m;
            foreach (timetable_request request in Requests)
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
				//newRequest.Duration = request.Duration;
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
                    weekListArray[week.Week - 1] = true;
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
                RequestList.Add(newRequest);
            }
            //var check = db.timetable_round.Where(r => r.Round_Status == "Current").First();
            //if (!(check is timetable_round))
            //{
            //    check = db.timetable_round.Last();
            //}
            List<Models.RequestListModel> FinalList = new List<Models.RequestListModel>();
            RequestList.ForEach(delegate(Models.RequestListModel request)
            {
                //if (request.Weeks[Week-1] && Week != 0)
                //{
                    FinalList.Add(request);
                //}
            });
            return FinalList;
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

        // GET api/RequestAPIController/5
        public IEnumerable<timetable_request> Gettimetable_requests(string RequestID)
        {

            var timetable_request = from m in db.timetable_request select m;
            timetable_request = timetable_request.Where(e => e.Request_ID.Equals(RequestID));

            if (timetable_request == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return timetable_request;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

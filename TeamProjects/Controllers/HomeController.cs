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
        [Authorize]
        public ActionResult Index()
        {
            team04Entities db = new team04Entities();
            int requestCount = db.timetable_request.Count();
            int toSkip = (requestCount > 5) ? requestCount - 5 : 0;
            ViewBag.RecentlyAdded = db.timetable_request.OrderBy(r => r.Request_ID).Skip(toSkip).OrderByDescending(r => r.Request_ID);
            ViewBag.Attention = db.timetable_request.Where(r => r.Request_Status == 3 || r.Request_Status == 4).OrderByDescending(r => r.Request_ID);
            ViewBag.RoundInfo = db.timetable_round.Where(r => r.Round_Status == "Current").FirstOrDefault();
            return View();
        }
    }
}

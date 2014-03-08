using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication1.Models; 

namespace WebApplication1.Controllers
{
    public class TestDBController : Controller
    {
        //
        // GET: /TestDB/
        public ActionResult Index()
        {
            var entities = new TestDBEntities();

            return View(entities.timetable_room.ToList());
        }

        public ActionResult View1()
        {
            ViewBag.Message = "Your view1 page.";

            return View();
        }
	}
}
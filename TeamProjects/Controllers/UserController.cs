using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TeamProjects.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn() {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(TeamProjects.Models.DepartmentModel department) {
        
        }

    }
}

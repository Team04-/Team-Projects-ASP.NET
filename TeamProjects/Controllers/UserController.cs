using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using TeamProjects.Models;

namespace TeamProjects.Controllers
{
    public class UserController : Controller
    {
        private string[][] getDepartmentsInfo()
        {
            team04Entities db = new team04Entities();
            string[] Department_Names = db.timetable_department.Select(name => name.Department_Name).ToArray();
            string[] Department_Codes = db.timetable_department.Select(name => name.Department_Code).ToArray();
            string[][] DepartmentsInfo = new string[2][];
            DepartmentsInfo[0] = Department_Names;
            DepartmentsInfo[1] = Department_Codes;
            return DepartmentsInfo;
        }
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LogIn() 
        {
            ViewBag.DepartmentsInfo = getDepartmentsInfo();
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(Models.DepartmentModel department) 
        {
            if (ModelState.IsValid)
            {
                if (isValid(department.Code, department.Password))
                {
                    FormsAuthentication.SetAuthCookie(department.Code, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("","Login data is incorrect.");
                }
            }
            return View(department);
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.DepartmentModel department)
        {
            if (ModelState.IsValid)
            {
                using (var db = new Models.team04Entities())
                {
                    var crypto = new SimpleCrypto.PBKDF2();
                    var encryptedPass = crypto.Compute(department.Password);

                    var newDep = db.timetable_department.Create();
                    newDep.Department_Code = department.Code;
                    newDep.Department_Name = department.Name;
                    newDep.Password = encryptedPass;
                    newDep.Password_Salt = crypto.Salt;
                    newDep.Round = db.timetable_round.Where(r => r.Round_Status == "Current").First().Round_Code;
                    db.timetable_department.Add(newDep);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
            }
            return View(department);
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        private bool isValid(string department, string password)
        {
            bool isValid = false;
            var crypto = new SimpleCrypto.PBKDF2();
            using (var db = new Models.team04Entities())
            {
                var user = db.timetable_department.FirstOrDefault(u => u.Department_Code == department);
                if (user != null)
                {
                    var hashedpassword = crypto.Compute(password, user.Password_Salt);
                    if (user.Password == hashedpassword)
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }
    }
}

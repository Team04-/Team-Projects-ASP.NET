using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

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
        public ActionResult LogIn() 
        {
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
                    var crypto = new Misc.BCryptHasher();
                    var salt = GenerateSaltValue();
                    HashAlgorithm bcrypt = new Misc.BCryptHasher();
                    var hashedpassword = HashPassword(department.Password, salt, bcrypt);
                    var newDep = db.timetable_department.Create();
                    newDep.Department_Code = department.Code;
                    newDep.Department_Name = department.Name;
                    newDep.Password = hashedpassword;
                    newDep.Password_Salt = salt;
                    db.timetable_department.Add(newDep);
                    db.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
            }
            return View(department);
        }

        public ActionResult LogOut()
        {
            return View();
        }

        private bool isValid(string department, string password)
        {
            bool isValid = false;
            using (var db = new Models.team04Entities())
            {
                var user = db.timetable_department.FirstOrDefault(u => u.Department_Code == department);
                if (user != null)
                {
                    HashAlgorithm bcrypt = new Misc.BCryptHasher();
                    if (user.Password == HashPassword(password, user.Password_Salt, bcrypt))
                    {
                        isValid = true;
                    }
                }
            }
            return isValid;
        }
        private static string GenerateSaltValue()
        {
            UnicodeEncoding utf16 = new UnicodeEncoding();

            if (utf16 != null)
            {
                // Create a random number object seeded from the value
                // of the last random seed value. This is done
                // interlocked because it is a static value and we want
                // it to roll forward safely.

                Random random = new Random(unchecked((int)DateTime.Now.Ticks));

                if (random != null)
                {
                    // Create an array of random values.

                    byte[] saltValue = new byte[2];

                    random.NextBytes(saltValue);

                    // Convert the salt value to a string. Note that the resulting string
                    // will still be an array of binary values and not a printable string. 
                    // Also it does not convert each byte to a double byte.

                    string saltValueString = utf16.GetString(saltValue);

                    // Return the salt value as a string.

                    return saltValueString;
                }
            }

            return null;
        }

        private static string HashPassword(string clearData, string saltValue, HashAlgorithm hash)
        {
            UnicodeEncoding encoding = new UnicodeEncoding();

            if (clearData != null && hash != null && encoding != null)
            {
                // If the salt string is null or the length is invalid then
                // create a new valid salt value.

                if (saltValue == null)
                {
                    // Generate a salt string.
                    saltValue = GenerateSaltValue();
                }

                int SaltValueSize = saltValue.Length;
                // Convert the salt string and the password string to a single
                // array of bytes. Note that the password string is Unicode and
                // therefore may or may not have a zero in every other byte.

                byte[] binarySaltValue = new byte[SaltValueSize];

                binarySaltValue[0] = byte.Parse(saltValue.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                binarySaltValue[1] = byte.Parse(saltValue.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                binarySaltValue[2] = byte.Parse(saltValue.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                binarySaltValue[3] = byte.Parse(saltValue.Substring(6, 2), System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);

                byte[] valueToHash = new byte[SaltValueSize + encoding.GetByteCount(clearData)];
                byte[] binaryPassword = encoding.GetBytes(clearData);

                // Copy the salt value and the password to the hash buffer.

                binarySaltValue.CopyTo(valueToHash, 0);
                binaryPassword.CopyTo(valueToHash, SaltValueSize);

                byte[] hashValue = hash.ComputeHash(valueToHash);

                // The hashed password is the salt plus the hash value (as a string).

                string hashedPassword = saltValue;

                foreach (byte hexdigit in hashValue)
                {
                    hashedPassword += hexdigit.ToString("X2", CultureInfo.InvariantCulture.NumberFormat);
                }

                // Return the hashed password as a string.

                return hashedPassword;
            }

            return null;
        }
    }
}

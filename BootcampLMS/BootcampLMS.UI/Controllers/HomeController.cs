using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootcampLMS.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");
            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Admin");

            if (User.IsInRole("Teacher"))
                return RedirectToAction("Index", "Teacher");

            if (User.IsInRole("Parent"))
                return RedirectToAction("Index", "Parent");

            if (User.IsInRole("Student"))
                return RedirectToAction("Index", "Student");
            
            // Home/Index is waiting for approval message
            return View();
        }
    }
}
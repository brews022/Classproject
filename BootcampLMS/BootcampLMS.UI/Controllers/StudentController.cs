using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BootcampLMS.Data.Repositories;
using BootcampLMS.UI.Models;
using Microsoft.AspNet.Identity;

namespace BootcampLMS.UI.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetCourseList()
        {
            string teacherId = User.Identity.GetUserId();

            CourseRepository repo = new CourseRepository();
            var all = repo.GetSummariesByTeacher(teacherId);

            RosterInfoViewModel vm = new RosterInfoViewModel();

            return View("Index", vm);
        }
    }
}
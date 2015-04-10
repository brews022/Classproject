using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;
using BootcampLMS.BLL;
using BootcampLMS.Data;
using BootcampLMS.Data.Repositories;
using BootcampLMS.Models;
using BootcampLMS.UI.Models;
using Microsoft.AspNet.Identity;

namespace BootcampLMS.UI.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return GetCourseList(false);
        }

        public ActionResult Archived()
        {
            return GetCourseList(true);
        }

        public ActionResult GetCourseList(bool archived)
        {
            string teacherId = User.Identity.GetUserId();

            CourseRepository repo = new CourseRepository();
            var all = repo.GetSummariesByTeacher(teacherId);

            TeacherDashboardViewModel vm = new TeacherDashboardViewModel();
            vm.Archived = all.Count (c=> c.IsArchived);
            vm.Current = all.Count(c=> !c.IsArchived);
            vm.Courses = all.Where(s=>s.IsArchived == archived).ToList();

            return View("Index", vm);
        }

        public ActionResult Course(int id)
        {
            CourseRepository cRepo = new CourseRepository();
            
            CourseInfoViewModel vm = new CourseInfoViewModel();

            vm.CourseId = id;
            vm.CourseInfo = cRepo.GetById(id);
            vm.IsEdit = true;
            //vm.Analytics = new CourseAnalytics();
            var getGrades = cRepo.GetGradeBookGrades(id);
            var grades = new Dictionary<string, int>
            {
                {"A", 0},
                {"B", 0},
                {"C", 0},
                {"D", 0},
                {"F", 0}
            };
            foreach (var grade in getGrades)
            {
                var gradeLetter = Grading.Grade(grade.TotalEarned, grade.TotalPossible);
                grades[gradeLetter] += 1;
            }

            vm.GetGrades = grades;
            vm.SudentCount = getGrades.Count;

            if (vm.CourseInfo == null)
                return RedirectToAction("Index");

            return View("CourseInfo", vm);
        }

        [HttpPost]
        public ActionResult EditCourse(CourseInfoViewModel infoCourse)
        {
            if (ModelState.IsValid)
            {
                var database = new CourseRepository();
                var course = new Course()
                {
                    CourseDescription = infoCourse.CourseInfo.CourseDescription,
                    Department = infoCourse.CourseInfo.Department,
                    Name = infoCourse.CourseInfo.Name,
                    GradeLevel = infoCourse.CourseInfo.GradeLevel,
                    IsArchived = infoCourse.CourseInfo.IsArchived,
                    StartDate = infoCourse.CourseInfo.StartDate,
                    EndDate = infoCourse.CourseInfo.EndDate,
                    CourseId = infoCourse.CourseInfo.CourseId,
                    TeacherId = infoCourse.CourseInfo.TeacherId
                };
                database.Update(course);
                return RedirectToAction("Index", new { id = infoCourse.CourseInfo.CourseId });
            }
            return View("CourseInfo", infoCourse);

        }

        public ActionResult AddCourse()
        {
            CourseInfoViewModel vm = new CourseInfoViewModel();
            vm.CourseId = 0;
            vm.IsEdit = false;
            vm.CourseInfo = new Course();
            vm.Analytics = null;

            return View("AddCourse");
        }

        [HttpPost]
        public ActionResult AddCourse(Course newCourse)
        {
            if (ModelState.IsValid)
            {
                newCourse.TeacherId = User.Identity.GetUserId();
                var database = new CourseRepository();
                database.Insert(newCourse);
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }

        public ActionResult Roster(int id)
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult CourseNavHeader(int id)
        {
            CourseRepository repo = new CourseRepository();

            var course = repo.GetById(id);
            return View(course);
        }
        
        [ChildActionOnly]
        public ActionResult CourseRosterTable(int id)
        {
            RosterRepository rosters = new RosterRepository();
            var vm = rosters.GetCourseStudents(id);
            return View(vm);
        }

        [ChildActionOnly]
        public ActionResult AddStudentToCourse(int id, AddStudentViewModel addStudent)
        {
            addStudent.CourseId = id;
            
            if (addStudent.LastName != null || addStudent.GradeLevel.HasValue)
            {
                UserProfileRepo userProfiles = new UserProfileRepo();
                addStudent.SearchResults = userProfiles.SearchByNotInCourse(id, addStudent.LastName, addStudent.GradeLevel);
            }

            return View(addStudent);
        }
        
        [HttpPost]
        public ActionResult AddStudentToCourse(Roster addStudent)
        {
            var database = new RosterRepository();
            database.Insert(addStudent);
                
            return RedirectToAction("Roster", new {id = addStudent.CourseId});
           
        }
        
        [HttpPost]
        public ActionResult DeleteStudentFromClass()
        {
            int studentId = int.Parse(Request.Form["RosterId"]);
            var database = new RosterRepository();
            database.Delete(studentId);

            int rosterCourseDeleteId = int.Parse(Request.Form["CourseId"]);
            
            return RedirectToAction("Roster", new {id = rosterCourseDeleteId});
        }

        public ActionResult Gradebook(int id)
        {
            GradebookInfoViewModel vm = new GradebookInfoViewModel();

            CourseRepository cRepo = new CourseRepository();

            vm.CourseId = id;
            vm.AssignmentInfo = cRepo.GetAssignmentsForCourses(id);
            vm.StudentAssignments = cRepo.GetGradebookStudents(id);
            vm.CurrentStudents = cRepo.GetGradeBookGrades(id);

            return View("Gradebook", vm);
        }

        public ActionResult AddAssignment(int id)
        {
            Assignment vm = new Assignment();

            vm.CourseId = id;
            vm.DueDate = DateTime.Now.AddDays(30);
            
            return View("AddAssignment", vm);
        }

        [HttpPost]
        public ActionResult AddAssignment(Assignment newAssignment)
        {
            if (ModelState.IsValid)
            {
                var database = new CourseRepository();
                database.AddAssignmentToCourse(newAssignment);
                return RedirectToAction("Gradebook", new {id = newAssignment.CourseId});
            }

            return View("AddAssignment", newAssignment);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootcampLMS.Models;

namespace BootcampLMS.UI.Models
{
    public class RosterInfoViewModel
    {
        public Course CourseInfo { get; set; }
        public List<CourseRoster> Roster { get; set; }
        public AddStudentViewModel AddStudent { get; set; }
    }

    public class AddStudentViewModel
    {
        public string LastName { get; set; }
        public int? GradeLevel { get; set; }
        public List<UserProfile> SearchResults { get; set; }
        public int CourseId { get; set; }
    }
}
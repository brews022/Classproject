using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootcampLMS.Models;

namespace BootcampLMS.UI.Models
{
    public class StudentDashboardViewModel
    {
       
        public List<CourseSummary> Courses { get; set; }
        public List<AssignmentTracker> Grade { get; set; } 
    }
}
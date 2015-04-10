using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootcampLMS.Models;

namespace BootcampLMS.UI.Models
{
    public class TeacherDashboardViewModel
    {
        public int Archived { get; set; }
        public List<CourseSummary> Courses { get; set; }
        public int Current { get; set; }
    }
}
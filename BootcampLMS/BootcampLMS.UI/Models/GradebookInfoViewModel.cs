using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootcampLMS.Models;

namespace BootcampLMS.UI.Models
{
    public class GradebookInfoViewModel
    {
        public List<CourseGradebook> StudentAssignments { get; set; }
        public List<CourseGradebook> CurrentStudents { get; set; }
        public int CourseId { get; set; }
        public string Name { get; set; }
        public Course CourseInfo { get; set; }
        public List<Assignment> AssignmentInfo { get; set; }
    }
}
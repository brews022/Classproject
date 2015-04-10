using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BootcampLMS.Models;

namespace BootcampLMS.UI.Models
{
    public class CourseInfoViewModel
    {
        public int CourseId { get; set; }
        public bool IsEdit { get; set; }
        public Course CourseInfo { get; set; }
        public CourseAnalytics Analytics { get; set; }
        public Dictionary<string, int> GetGrades { get; set; }
        public int SudentCount { get; set; }
    }
}
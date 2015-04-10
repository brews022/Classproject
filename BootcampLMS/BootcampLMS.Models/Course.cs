using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampLMS.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string TeacherId { get; set; }

        [DisplayName("Class Name")]
        public string Name { get; set; }

        [DisplayName("Subject")]
        public string Department { get; set; }

        [DisplayName("Description")]
        public string CourseDescription { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Grade Level")]
        public int? GradeLevel { get; set; }

        [DisplayName("Archive")]
        public bool IsArchived { get; set; }
    }
}

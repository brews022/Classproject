using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampLMS.Models
{
    public class CourseGradebook
    {
        public int RosterId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Assignment> AssignmentDisplay { get; set; }
        public int TotalEarned { get; set; }
        public int TotalPossible { get; set; }
        public int AssignmentId { get; set; }
        public int AssignmentTrackerId { get; set; }

    }
}

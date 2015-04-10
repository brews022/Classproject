using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;



namespace BootcampLMS.Models
{
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Please enter a Course Name")]
        [DisplayName("Course Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description")]
        [DisplayName("Description")]
        public string AssignmentDescription { get; set; }

        
        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Please enter points possible")]
        [DisplayName("Points Possible")]
        public int PointsPossible { get; set; }
    }
}

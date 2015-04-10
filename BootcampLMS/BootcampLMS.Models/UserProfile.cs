using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace BootcampLMS.Models
{
    public class UserProfile
    {
        public string UserId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string RequestedRole { get; set; }
        public int? GradeLevel { get; set; }
    }
}
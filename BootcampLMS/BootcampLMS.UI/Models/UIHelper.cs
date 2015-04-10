using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BootcampLMS.UI.Models
{
    public static class UIHelper
    {
        public static IEnumerable<SelectListItem> DropdownRoles
        {
            get
            {
                return new SelectListItem[]
                {
                    new SelectListItem {Text = "Student", Value = "Student"},
                    new SelectListItem {Text = "Parent", Value = "Parent"},
                    new SelectListItem {Text = "Teacher", Value = "Teacher"}
                };
            }
        }

        public static IEnumerable<SelectListItem> DropdownGradeLevels
        {
            get
            {
                List<SelectListItem> gradeLevels = new List<SelectListItem>();
            gradeLevels.Add(new SelectListItem{Text = "N/A", Value = "-1"});
            gradeLevels.Add(new SelectListItem{Text = "Kindergarden", Value = "0"});
            for (int i = 1; i <= 8; i++)
                gradeLevels.Add(new SelectListItem{ Text = i.ToString(), Value = i.ToString() });
            gradeLevels.Add(new SelectListItem{Text = "Freshman", Value = "9"});
            gradeLevels.Add(new SelectListItem { Text = "Sophmore", Value = "10" });
            gradeLevels.Add(new SelectListItem { Text = "Junior", Value = "11" });
            gradeLevels.Add(new SelectListItem { Text = "Senior", Value = "12" });

                return gradeLevels;
            }
        }
    }
}
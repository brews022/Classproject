using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampLMS.Models;

namespace BootcampLMS.BLL
{
    public static class Grading
    {
        public static decimal Percentage(decimal earned, int possible)
        {
            if (possible == 0)
                return 0;

            return (decimal)earned / (decimal)possible * 100;
        }

        public static string Percentage(AssignmentTracker at, Assignment a)
        {
            
            decimal pct = Percentage(at.EarnedPoints, a.PointsPossible);
            return pct.ToString("0.0") + "%";
        }

        public static string Grade(decimal percentage)
        {
            if (percentage >= 90M)
                return "A";
            if (percentage >= 80M)
                return "B";
            if (percentage >= 70M)
                return "C";
            if (percentage >= 60M)
                return "D";
            return "F";
        }

        public static string Grade(decimal earned, int possible)
        {
            return Grade(Percentage(earned, possible));
        }

        public static string Grade(AssignmentTracker at, Assignment a)
        {
            return Grade(at.EarnedPoints, a.PointsPossible);
        }
    }
}

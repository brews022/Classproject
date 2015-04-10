using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampLMS.Data.Repositories;

namespace BootcampLMS.Data
{
    public class Repos
    {
        private ParentStudentRepository _parentStudent;
        private RosterRepository _roster;

        public ParentStudentRepository ParentStudent
        {
            get
            {
                if (_parentStudent == null)
                    _parentStudent = new ParentStudentRepository();
                return _parentStudent;
            }
        }

        public RosterRepository Roster
        {
            get
            {
                if (_roster == null)
                    _roster = new RosterRepository();
                return _roster;

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using BootcampLMS.Data.Repositories;
using BootcampLMS.Models;

namespace BootcampLMS.UI.Controllers
{
    public class GradebookController : ApiController
    {
        public AssignmentTracker PostGrade(AssignmentTracker at)
        {
            AssignmentTrackerRepo repo = new AssignmentTrackerRepo();

            if (at.Id == 0)
                repo.Insert(at);
            else
                repo.Update(at);
            return at;
        }

        public AssignmentTracker GetGrade(int id)
        {
            AssignmentTrackerRepo repo = new AssignmentTrackerRepo();
            return repo.GetById(id);
        }
    }
}
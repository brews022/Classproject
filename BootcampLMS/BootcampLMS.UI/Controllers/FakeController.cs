using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Configuration;
using BootcampLMS.Data;
using BootcampLMS.Data.Repositories;
using BootcampLMS.Models;

namespace BootcampLMS.UI.Controllers
{
    public class FakeController : Controller
    {
        // GET: Fake
        //public ActionResult TestUser()
        //{
        //    UserProfileRepo repo = new UserProfileRepo();

        //    var newUser = new UserProfile()
        //    {
        //        FirstName = "",
        //        LastName = "",
        //        RequestedRole = "",
        //        UserId = "fake"
        //    };
        //}
        
        //public ActionResult TestRoster()
        //{
        //    RosterRepository repo = new RosterRepository();

        //    var newRoster = new Roster()
        //    {
        //        CourseId = 2,
        //        IsActive = true,
        //        UserId = "fake"
        //    };

        //    repo.Insert(newRoster);

        //    return Json(newRoster);
        //}

        //protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        //{
        //    return base.Json(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PartyRegistration.Models;

namespace PartyRegistration.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/REgistrationForm
        public ActionResult RegistrationForm2()
        {
            var model = new Rsvpresponse();

            return View(model);
        }
        //POST: /Home/RegistrationForm
        [HttpPost]
        public ActionResult RegistrationForm(Rsvpresponse model)
        {
            if (ModelState.IsValid)
                return View("Confirmation", model);
            else
                return View(model);
        }
	}
}
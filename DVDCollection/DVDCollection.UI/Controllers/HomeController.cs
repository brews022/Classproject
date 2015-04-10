using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DVDCollection.UI.Models;
using DVDCollection.UI.Repositories;

namespace DVDCollection.UI.Controllers
{
    public class HomeController : Controller
    {
        private IFilmRepository database = new FakeFilmDatabase();
        
        
        // GET: /Home/
        public ActionResult Index()
        {
            
            var films = database.GetAll();
            return View(films);
        }

        public ActionResult Add()
        {
            var model = new Film();
            return View(model);
        }

        [HttpPost]
        public ActionResult AddFilm(Film c)
        {
            var database = new FakeFilmDatabase();
            
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            //int filmId = int.Parse(RouteData.Values["id"].ToString());

            var database = new FakeFilmDatabase();
            var film = database.GetById(id);

            return View(film);
        }

        [HttpPost]
        public ActionResult EditFilm(Film c)
        {
            var database = new FakeFilmDatabase();

            

            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult DeleteFilm()
        {
            int filmId = int.Parse(Request.Form["FilmId"]);

            var database = new FakeFilmDatabase();
            

            var films = database.GetAll();
            return View("Index", films);
        }

        [HttpPost]
        public ActionResult Details(Film c)
        {
            
            return View();
        }


        public ActionResult Details(int id)
        {
            var database = new FakeFilmDatabase();
            Film film = database.GetById(id);
            return View("Details", film);
        }

        [HttpPost]
        public ActionResult Search(string searchBy, string searchType)
        {
            var database = new FakeFilmDatabase();

            IEnumerable<Film> results;

            if (searchType == "Title")
            {
                results = database.GetAll().Where(m => m.Title.Contains(searchBy));
                return View("IndexSearch", results);
            }

            if (searchType == "Director")
            {
                results = database.GetAll().Where(m => m.Director.Contains(searchBy));
                return View("IndexSearch", results);
            }

            if (searchType == "Actors")
            {
                results = database.GetAll().Where(m => m.Actor.Contains(searchBy));
                return View("IndexSearch", results);
            }

            return View("Index", database.GetAll());
        }
	}
}

//
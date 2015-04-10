using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DVDCollection.UI.Repositories;

namespace DVDCollection.UI.Models
{
    public class FakeFilmDatabase : IFilmRepository
    {
        private static List<Film> _films = new List<Film>();

        public List<Film> GetAll()
        {
            return _films;
        }

        public void Add(Film film)
        {
            if (_films.Any())
                film.FilmId = _films.Max(c => c.FilmId) + 1;
            else
                film.FilmId = 1;
            
            _films.Add(film);
        }

        public void Delete(int id)
        {
            _films.RemoveAll(c => c.FilmId == id);
        }

        public void Edit(Film film)
        {
            Delete(film.FilmId);
            _films.Add(film);
        }

        public Film GetById(int id)
        {
            return _films.First(c => c.FilmId == id);
        }
    }
}
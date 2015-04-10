using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDCollection.UI.Models;

namespace DVDCollection.UI.Repositories
{
    public interface IFilmRepository
    {
        List<Film> GetAll();
        void Add(Film film);
        void Delete(int id);
        void Edit(Film film);
        Film GetById(int id);
    }
}

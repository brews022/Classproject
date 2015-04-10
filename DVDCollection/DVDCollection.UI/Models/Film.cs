using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDCollection.UI.Models
{
    public class Film
    {
        public int FilmId { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Actor { get; set; }
        public int Year { get; set; }
        public int RunTime { get; set; }
        public string Rating { get; set; }

    }
}
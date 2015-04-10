//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Configuration;
//using System.Web.UI.WebControls;
//using DVDCollection.UI.Models;

//namespace DVDCollection.UI.Repositories
//{
    
//    public class DBFilmRepository : IFilmRepository
//    {
//        private string connectionstring = ConfigurationManager
//            .ConnectionStrings["DvdDatabase"].ConnectionString;

//        public List<Models.Film> GetAll()
//        {
//            using (SqlConnection cn = new SqlConnection(connectionstring))
//            {
//                SqlCommand cmd = new SqlCommand();
//                cmd.CommandText = "SELECT * FROM DVDCollection";
//                cmd.Connection = cn;

//                cn.Open();

//                using (SqlDataReader dr = cmd.ExecuteReader())
//                {
                  
//                    while (dr.Read())
//                    {
//                          var Film = new Film();
//                            Film.FilmId = dr())
//                    }
//                }
//            }

//        }

       
//        public void Add(Models.Film film)
//        {
//            throw new NotImplementedException();
//        }

//        public void Delete(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void Edit(Models.Film film)
//        {
//            throw new NotImplementedException();
//        }

//        public Models.Film GetById(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
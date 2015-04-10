using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using BootcampLMS.Models;


namespace BootcampLMS.Data
{
    public class UserProfileRepo
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public List<UserProfile> GetAll()
        {
            List<UserProfile> list = new List<UserProfile>();

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = " SELECT * FROM UserProfile";
                cmd.Connection = cn;

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        list.Add(PopulateFromDataReader(dr));
                    }
                }
            }
            return list;
        }

        public UserProfile GetById(int userId)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText =
                    @"select *
                        from UserProfile
                        where UserId =@UserId";

                cmd.Parameters.AddWithValue("@UserId", userId);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        return PopulateFromDataReader(dr);
                    }
                }

            }
            return null;
        }

        private UserProfile PopulateFromDataReader(SqlDataReader dr)
        {
            UserProfile u = new UserProfile();
            u.UserId = dr["UserId"].ToString();
            u.FirstName = dr["FirstName"].ToString();
            u.LastName = dr["LastName"].ToString();
            u.RequestedRole = dr["RequestedRole"].ToString();
            
            if (dr["GradeLevel"] != DBNull.Value)
            {
                u.GradeLevel = (int) dr["GradeLevel"];
            }
            
            return u;
        }

        public void AddUser(UserProfile u)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText =
                    @"insert into UserProfile (UserId, FirstName, LastName, RequestedRole, GradeLevel)
                        values (@UserId, @FirstName, @LastName, @RequestedRole, @GradeLevel)";

                cmd.Parameters.AddWithValue("@UserId", u.UserId);
                cmd.Parameters.AddWithValue("@FirstName", u.FirstName);
                cmd.Parameters.AddWithValue("@LastName", u.LastName);
                cmd.Parameters.AddWithValue("@RequestedRole", u.RequestedRole);
                cmd.Parameters.AddWithValue("@GradeLevel", u.GradeLevel);
                
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditUser(UserProfile u)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText =
                    @"update UserProfile set FirstName = @FirstName, 
                        LastName = @LastName, 
                        RequestedRole = @RequestedRole, 
                        GradeLevel = @GradeLevel,                         
                        where UserId = @UserId";

                cmd.Parameters.AddWithValue("@UserId", u.UserId);
                cmd.Parameters.AddWithValue("@FirstName", u.FirstName);
                cmd.Parameters.AddWithValue("@LastName", u.LastName);
                cmd.Parameters.AddWithValue("@RequestedRole", u.RequestedRole);
                cmd.Parameters.AddWithValue("@GradeLevel", u.GradeLevel);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteUser(string userId)
        {
            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                var cmd = cn.CreateCommand();
                cmd.CommandText =
                    @"delete u from UserProfile u where u.UserId = @UserId";

                cmd.Parameters.AddWithValue("@UserId", userId);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Configuration;
using BootcampLMS.Data.Repositories;
using BootcampLMS.Models;
using Dapper;


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

        public List<UserProfile> SearchByNotInCourse(int courseId, string lastName, int? gradeLevel)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = @"Select UP.*, Ro.*
                    from UserProfile UP
                    inner join AspNetUserRoles UR
                        on UP.UserId = UR.UserId
                    Inner join AspNetRoles R
                        on UR.ROleId = R.Id
                    Left join Roster Ro
                        on UP.UserId = Ro.UserId AND Ro.CourseId = @CourseId and Ro.IsActive = 1
                    Where R.Name = 'Student'
                        and Ro.RosterId is null";

                DynamicParameters dp = new DynamicParameters();
                dp.Add("@CourseId", courseId);

                if (!String.IsNullOrEmpty(lastName))
                {
                    sql += " and LastName like @LastName";
                    dp.Add("@LastName", "%" + lastName + "%");
                }

                if (gradeLevel.HasValue)
                {
                    sql += " and GradeLevel = @GradeLevel";
                    dp.Add("@GradeLevel", gradeLevel);
                }

                return conn.Query<UserProfile>(sql, dp).ToList();
            }
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

//        public List<UserProfile> GetByLastName(string lastName, int gradeLevel)
//        {
//            List<UserProfile> list = new List<UserProfile>();

//            using (SqlConnection cn = new SqlConnection(connectionString))
//            {
//                var cmd = cn.CreateCommand();
//                cmd.CommandText =
//                    @"select *
//                    from Userprofile
//                    where LastName = @Lastname and GradeLevel = @GradeLevel";
//                cmd.Parameters.AddWithValue("@LastName", lastName);
//                cmd.Parameters.AddWithValue("@GradeLevel", gradeLevel);

//                cn.Open();

//                using (SqlDataReader dr = cmd.ExecuteReader())
//                {
//                    while (dr.Read())
//                    {
//                        list.Add(PopulateFromDataReader(dr));
//                    }
//                }
//            }
//            return list;
//        }

        private UserProfile PopulateFromDataReader(SqlDataReader dr)
        {
            UserProfile u = new UserProfile();
            u.UserId = dr["UserId"].ToString();
            u.FirstName = dr["FirstName"].ToString();
            u.LastName = dr["LastName"].ToString();
            u.RequestedRole = dr["RequestedRole"].ToString();
            //u.GradeLevel = dr["GradeLevel"].ToString();

            if (dr["GradeLevel"] != DBNull.Value)
            {
                u.GradeLevel = (int)dr["GradeLevel"];
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
                cmd.Parameters.AddWithValue("@GradeLevel", u.GradeLevel.HasValue ? (object)u.GradeLevel : DBNull.Value);

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
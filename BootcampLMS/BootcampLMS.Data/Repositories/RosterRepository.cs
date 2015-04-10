using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BootcampLMS.Models;
using Dapper;

namespace BootcampLMS.Data.Repositories
{
    public class RosterRepository
    {
        public List<Roster> GetAll()
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "select * from Roster";
                return conn.Query<Roster>(sql).ToList();
            }
        }

        public Roster GetById(int rosterId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "select * from Roster where RosterId = @RosterId";
                return conn.Query<Roster>(sql, new {RosterId = rosterId}).FirstOrDefault();
            }
        }

        public void Insert(Roster roster)
        {
            roster.IsActive = true;

            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "insert into Roster (UserId, CourseId, IsActive) values (@UserId, @CourseId, @IsActive); select CAST(SCOPE_IDENTITY() as int)";
                roster.RosterId = (int)conn.ExecuteScalar(sql, roster);
            }
        }

        public void Update(Roster roster)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "update Roster set UserId = @UserId, CourseId = @CourseId, IsActive = @IsActive where RosterId = @RosterId";
                conn.Execute(sql, roster);
            }
        }

        public void Delete(int rosterId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "delete from Roster where RosterId = @RosterId";
                conn.Execute(sql, new {RosterId = rosterId});
            }
        }

        public List<CourseRoster> GetCourseStudents(int courseId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = @"select R.RosterId, R.UserId, UP.FirstName, UP.LastName, U.Email, R.CourseId
                                from Roster R
                                    inner join UserProfile UP on R.UserId = UP.UserId
                                    inner join AspNetUsers U on UP.UserId = U.Id
                                Where R.CourseId = @CourseId
                                    and R.IsActive = 1";
                return conn.Query<CourseRoster>(sql, new { CourseId = courseId }).ToList();
            }
        }
    }
}

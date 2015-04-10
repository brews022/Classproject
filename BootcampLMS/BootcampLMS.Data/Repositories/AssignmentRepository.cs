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
    public class AssignmentRepository
    {
        public List<Assignment> GetAll()
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "select * from Assignment";
                return conn.Query<Assignment>(sql).ToList();
            }
        }

        public Assignment GetById(int assignmentId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "select * from Assignment where AssignmentId = @AssignmentId";
                return conn.Query<Assignment>(sql, new { AssignmentId = assignmentId }).FirstOrDefault();
            }
        }

        public void Insert(Assignment assignment)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "insert into Assignment (CourseId, Name, Description, DueDate, PointsPossible) values (@CourseId, " +
                             "@Name, @AssignmentDescription, @DueDate, @PointsPossible); select CAST(SCOPE_IDENTITY() as int)";
                assignment.AssignmentId = (int)conn.ExecuteScalar(sql, assignment);
            }
        }

        public void Update(Assignment assignment)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "update Assignment set CourseId = @CourseId, Name = @Name, AssignmentDescription = @AssignmentDescription" +
                             " DueDate = @DueDate, PointsPossible = @PointsPossible where AssignmentId = @AssignmentId";
                conn.Execute(sql, assignment);
            }
        }

        public void Delete(int assignmentId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "delete from assignment where AssignmentId = @AssignmentId";
                conn.Execute(sql, new { AssignmentId = assignmentId });
            }
        }
    }
}

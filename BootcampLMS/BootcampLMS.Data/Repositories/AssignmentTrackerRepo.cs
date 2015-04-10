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
    public class AssignmentTrackerRepo
    {
        public List<AssignmentTracker> GetAll()
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "select * from AssignmentTracker";
                return conn.Query<AssignmentTracker>(sql).ToList();
            }
        }

        public AssignmentTracker GetById(int idId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "select * from AssignmentTracker where idId = @idId";
                return conn.Query<AssignmentTracker>(sql, new { idId = idId }).FirstOrDefault();
            }
        }

        public void Insert(AssignmentTracker assignmentTracker)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "insert into AssignmentTracker (AssignmentId, RosterId, EarnedPoints) values (@AssignmentId, @RosterId, @EarnedPoints); select CAST(SCOPE_IDENTITY() as int)";
                assignmentTracker.Id = (int)conn.ExecuteScalar(sql, assignmentTracker);
            }
        }

        public void Update(AssignmentTracker assignmentTracker)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "update AssignmentTracker set AssignmentId = @AssignmentId, RosterId = @RosterId, EarnedPoints = @EarnedPoints where Id = @Id";
                conn.Execute(sql, assignmentTracker);
            }
        }

        public void Delete(int idId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "delete from assignmentTracker where Id = @Id";
                conn.Execute(sql, new { Id = idId });
            }
        }

    }
}

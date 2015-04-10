using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace BootcampLMS.Data.Repositories
{
    public class ParentStudentRepository
    {
        public void Add(string parentId, string studentId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "insert into ParentStudent (ParentId, StudentId) values (@ParentId, @StudentId)";
                conn.Execute(sql, new {ParentId = parentId, StudentId = studentId});
            }
        }

        public void Delete(string parentId, string studentId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "delete from ParentStudent where ParentId = @ParentId and StudentId = @StudentId)";
                conn.Execute(sql, new {ParentId = parentId, StudentId = studentId});
            }
        }
    }
}

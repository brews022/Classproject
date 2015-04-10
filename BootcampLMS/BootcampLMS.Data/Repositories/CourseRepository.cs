using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using BootcampLMS.Models;
using Dapper;


namespace BootcampLMS.Data.Repositories
{
    public class CourseRepository
    {
        public List<Course> GetAll()
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "select * from Course";
                return conn.Query<Course>(sql).ToList();
            }
        }

        public Course GetById(int courseId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "select * from Course where CourseId = @CourseId";
                return conn.Query<Course>(sql, new { CourseId = courseId }).FirstOrDefault();
            }
        }

        public IEnumerable<CourseSummary> GetSummariesByTeacher(string teacherId)
        {
            string sql = @"select C.CourseId, C.Name, C.IsArchived, count(R.UserId) as StudentCount
                            from Course C
                                left join Roster R on C.CourseId = R.CourseId
                            where TeacherId = @TeacherId                                
                            group by C.CourseId, C.Name, C.IsArchived
                            order by C.Name";

            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                return conn.Query<CourseSummary>(sql, new {TeacherId = teacherId});
            }
        }

        public void Insert(Course course)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "insert into Course (TeacherId, Name, Department, CourseDescription, StartDate, EndDate, GradeLevel, IsArchived) values " +
                             "(@TeacherId, @Name, @Department, @CourseDescription, @StartDate, @EndDate, @GradeLevel, @IsArchived); select CAST(SCOPE_IDENTITY() as int)";
                course.CourseId = (int)conn.ExecuteScalar(sql, course);
            }
        }

        public void Update(Course course)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "update Course set TeacherId = @TeacherId, Name = @Name, Department = @Department, CourseDescription= @CourseDescription, " +
                             "StartDate = @StartDate, EndDate = @EndDate, GradeLevel = @GradeLevel, IsArchived = @IsArchived where CourseId = @CourseId";
                conn.Execute(sql, course);
            }
        }

        public void Delete(int courseId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "delete from Course where CourseId = @CourseId";
                conn.Execute(sql, new { CourseId = courseId });
            }
        }

        public void AddAssignmentToCourse(Assignment assignment)
        {

            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = "insert into Assignment (CourseId, Name, PointsPossible, DueDate, Description) values " +
                             "(@CourseId, @Name, @PointsPossible, @DueDate, @AssignmentDescription); select CAST(SCOPE_IDENTITY() as int)";
                assignment.AssignmentId = (int)conn.ExecuteScalar(sql, assignment);

            }
        }

        public List<CourseGradebook> GetGradebookStudents(int courseId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = @"Select U.Userid, U.FirstName, U.LastName, AT.EarnedPoints as TotalEarned, A.PointsPossible as TotalPossible, R.RosterId, A.AssignmentId, AT.Id as AssignmentTrackerId
	                    From UserProfile U
		                    Inner Join Roster R
			                    on U.UserId = R.UserId
		                    Inner Join Assignment a
			                    on R.CourseId = A.CourseId
		                    Inner Join AssignmentTracker AT
			                    on R.RosterId = AT.RosterId and a.assignmentid = at.assignmentid
	                    where R.CourseId = @CourseId and R.IsActive = 1	                   
	                    Order By U.LastName";

                return conn.Query<CourseGradebook>(sql, new { CourseId = courseId }).ToList();
            }
        }

        public List<CourseGradebook> GetGradeBookGrades(int courseId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = @"Select U.UserId, U.FirstName, U.LastName, IsNull(SUM(AT.EarnedPoints), 0) as TotalEarned, IsNull(sum(a.PointsPossible),0) as TotalPossible, R.RosterId
	                        From UserProfile U
		                        Inner Join Roster R
			                        on U.UserId = R.UserId
		                        left Join Assignment a
			                        on R.CourseId = A.CourseId
		                        left Join AssignmentTracker AT
			                        on R.RosterId = AT.RosterId and a.assignmentid = at.assignmentid
	                        where R.CourseId = @CourseId and R.IsActive = 1
	                        Group By U.UserId, U.FirstName, U.LastName, R.RosterId
	                        Order By U.LastName";
                
                return conn.Query<CourseGradebook>(sql, new { CourseId = courseId }).ToList();
            }
        }

        public List<Assignment> GetAssignmentsForCourses(int courseId)
        {
            using (var conn = new SqlConnection(Settings.ConnectionString))
            {
                string sql = @"Select * From Assignment where CourseId = @CourseId Order By DueDate";

                return conn.Query<Assignment>(sql, new { CourseId = courseId }).ToList();
            }
        }
    }
}

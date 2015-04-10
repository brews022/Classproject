using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Web.UI.WebControls;
using BootcampLMS.Data;
using BootcampLMS.Data.Repositories;
using BootcampLMS.Models;
using BootcampLMS.UI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BootcampLMS.UI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BootcampLMS.UI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            // Don't want to redo this multiple times, only if we drop the database and need to re-seed
            if (new UserProfileRepo().GetAll().Any())
                return;

            CreateRole(context, "Admin");
            CreateRole(context, "Parent");
            CreateRole(context, "Student");
            CreateRole(context, "Teacher");

            string fakeTeacherId = CreateUser(context, "teacher@test.com", "password123", "Test", "Teacher", "Teacher", null);
            string fakeStudentId = CreateUser(context, "student@test.com", "password123", "Test", "Student", "Student", null);
            string fakeParentId = CreateUser(context, "parent@test.com", "password123", "Test", "Parent", "Parent", null);

            ParentStudentRepository psRepo = new ParentStudentRepository();
            psRepo.Add(fakeParentId, fakeStudentId);

            var algebra = new Course()
            {
                TeacherId = fakeTeacherId,
                Name = "Algebra 1",
                Department = "Math",
                CourseDescription = "Math and math related stuff",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(90)
            };

            var history = new Course()
            {
                TeacherId = fakeTeacherId,
                Name = "America",
                Department = "Social Studies",
                CourseDescription = "lots of old people",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(90)
            };

            CourseRepository cRepo = new CourseRepository();
            RosterRepository rRepo = new RosterRepository();
            AssignmentRepository aRepo = new AssignmentRepository();
            AssignmentTrackerRepo atRepo = new AssignmentTrackerRepo();

            cRepo.Insert(algebra);
            cRepo.Insert(history);

            List<string> moreStudentsIds = new List<string>();
            for (int i = 0; i < 50; i++)
            {
                string newStudentId = CreateUser(context, "fakestudent." + i + "@test.com", "password123", "Fake", "Student" + i, "Student", i%13);
                moreStudentsIds.Add(newStudentId);

                if (i % 10 == 0)
                    rRepo.Insert(new Roster() {CourseId = history.CourseId, IsActive = true, UserId = newStudentId});

                if (i%8 == 0) 
                    rRepo.Insert(new Roster() { CourseId = algebra.CourseId, IsActive = true, UserId = newStudentId });
            }

            var rosterInfo = new Roster
            {
                CourseId = algebra.CourseId,
                UserId = fakeStudentId,
                IsActive = true
            };

            rRepo.Insert(rosterInfo);

            var assignment = new Assignment
            {
                AssignmentDescription = "First Assignment Description",
                CourseId = algebra.CourseId,
                DueDate = DateTime.Now.AddDays(90),
                Name = "First Assignment",
                PointsPossible = 45

            };

            aRepo.Insert(assignment);

            var assignment2 = new Assignment
            {
                AssignmentDescription = "Second Assignment Description",
                CourseId = algebra.CourseId,
                DueDate = DateTime.Now.AddDays(90),
                Name = "Second Assignment",
                PointsPossible = 50
            };
                
            aRepo.Insert(assignment2);

            var grade = new AssignmentTracker
            {
                AssignmentId = assignment.AssignmentId,
                EarnedPoints = 40,
                RosterId = rosterInfo.RosterId
            };

            atRepo.Insert(grade);

            var grade2 = new AssignmentTracker
            {
                AssignmentId = assignment2.AssignmentId,
                EarnedPoints = 47,
                RosterId = rosterInfo.RosterId
            };

            atRepo.Insert(grade2);

        }

        private void CreateRole(ApplicationDbContext context, string role)
        {
            using (var roleStore = new RoleStore<IdentityRole>(context))
            {
                using (var roleManager = new RoleManager<IdentityRole>(roleStore))
                {
                    roleManager.Create(new IdentityRole { Name = role });
                }
            }
        }

        private string CreateUser(ApplicationDbContext context, string email, string password, string firstName, string lastName, string reqRole, int? gradeLevel)
        {
            // We already have the context, the EntityFramework object that talks to the database tables

            // First, the "store" that Microsoft puts in the middle. Don't ask me why.
            using (var userStore = new UserStore<ApplicationUser>(context))
            {
                // Next the manager, that actually does useful things. Like create user, add user to role, etc.
                using (var userMgr = new UserManager<ApplicationUser>(userStore))
                {
                    // This is the stub object, the bare minimum for an ASP.NET "User"
                    ApplicationUser user = new ApplicationUser
                    {
                        Email = email,
                        UserName = email
                    };

                    // Hey EntityFramework, please create the AspNetUsers row, and figure out the user id
                    userMgr.Create(user, password);
                    userMgr.AddToRole(user.Id, reqRole);

                    // Now that we have the UserId, we can create our UserProfile record and insert it with our repository
                    UserProfile profile = new UserProfile
                    {
                        UserId = user.Id,
                        FirstName = firstName,
                        LastName = lastName,
                        RequestedRole = reqRole,
                        GradeLevel = gradeLevel
                    };
                    UserProfileRepo repo = new UserProfileRepo();
                    repo.AddUser(profile);

                    // Now finally, return the user id so we can create Courses, etc.
                    return user.Id;
                }
            }
        }
    }
}

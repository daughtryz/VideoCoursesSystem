using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.ViewModels.Courses;

namespace VideoCoursesSystem.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly ApplicationDbContext _db;

        public StudentsService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<UserCourse> GetStudentActivities()
        {
            return _db.UserCourses.ToList();
        }

        public IEnumerable<Grade> GetStudentGrades()
        {
            return _db.Grades.ToList();
        }
    }
}

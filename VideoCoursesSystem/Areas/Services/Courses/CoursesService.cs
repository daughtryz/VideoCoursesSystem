using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data;
using VideoCoursesSystem.Data.Models;

namespace VideoCoursesSystem.Areas.Services.Teachers
{
    public class CoursesService : ICoursesService
    {
        private readonly ApplicationDbContext _db;
        public CoursesService(ApplicationDbContext db)
        {
            _db = db;
        }
        public IEnumerable<Course> AllCourses() => _db.Courses.OrderByDescending(c => c.StartDate).ToList();
        public Course CourseById(string id) 
        {
            var currentCourse = _db.Courses.FirstOrDefault(c => c.Id == id);
            currentCourse.Viewers++;
            _db.Courses.Update(currentCourse);
             _db.SaveChanges();
            return _db.Courses.FirstOrDefault(c => c.Id == id);
        } 
        
        public async Task CreateCourseAsync(string title, string component, string description, DateTime startDate, DateTime endDate)
        {
            Course course = new Course
            {
                Title = title,
                Component = component,
                Description = description,
                StartDate = startDate,
                EndDate = endDate
            };
            await _db.Courses.AddAsync(course);
            await _db.SaveChangesAsync();
        }
    }
}

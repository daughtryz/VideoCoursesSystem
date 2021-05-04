using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data.Models;

namespace VideoCoursesSystem.Areas.Services.Teachers
{
    public interface ICoursesService
    {
        Task CreateCourseAsync(string title, string component, string description, DateTime startDate, DateTime endDate);

        IEnumerable<Course> AllCourses();

        Course CourseById(string id);
    }
}

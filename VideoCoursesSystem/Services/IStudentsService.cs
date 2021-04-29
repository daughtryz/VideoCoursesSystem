using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.ViewModels.Courses;

namespace VideoCoursesSystem.Data.Models
{
    public interface IStudentsService
    {
        IEnumerable<UserCourse> GetStudentActivities();
        IEnumerable<Grade> GetStudentGrades();
    }
}

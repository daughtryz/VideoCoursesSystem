using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.ViewModels.Courses
{
    public class CourseListViewModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.ViewModels.Courses
{
    public class LogInformationViewModel
    {
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<string> StudentsIds { get; set; }
    }
}

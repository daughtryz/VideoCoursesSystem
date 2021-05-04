using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.Services.Helpers
{
    public interface IHelperService
    {
        string GetCourseId();
        void AddCourseId(string courseId);
    }
}

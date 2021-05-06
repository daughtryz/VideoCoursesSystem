using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.Services.Helpers
{
    public interface IHelperService
    {
        string GetCourseId();
        string GetExerciseId();

        void AddCourseId(string courseId);
        void AddExerciseId(string exerciseId);

    }
}

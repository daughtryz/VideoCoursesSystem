using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.Services.Helpers
{
    public class HelperService : IHelperService, IDisposable
    {
        public HelperService()
        {
            this.CourseIds = new List<string>();
            this.ExerciseIds = new List<string>();

        }
        public List<string> CourseIds { get; set; }

        public List<string> ExerciseIds { get; set; }


        public void AddCourseId(string courseId)
        {
            this.CourseIds.Add(courseId);
        }

        public void AddExerciseId(string exerciseId)
        {
            this.ExerciseIds.Add(exerciseId);
        }

        public void Dispose()
        {
            this.CourseIds = new List<string>();
            this.ExerciseIds = new List<string>();

        }

        public string GetCourseId()
        {
            var currentId = this.CourseIds.FirstOrDefault();
            return currentId;
        }

        public string GetExerciseId()
        {
            var currentId = this.ExerciseIds.FirstOrDefault();
            return currentId;
        }

    }
}

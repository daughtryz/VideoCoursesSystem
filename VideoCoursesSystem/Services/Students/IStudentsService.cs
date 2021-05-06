using Microsoft.AspNetCore.Http;
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
        public Task UploadExerciseAsync(IEnumerable<IFormFile> exercises, string studentId,string courseId);
        public Task EditExerciseAsync(IEnumerable<IFormFile> exercises, string exerciseId);
        IEnumerable<Exercise> AllExercises();
        Exercise GetExerciseById(string id);

        IEnumerable<Exercise> GetStudentExercises(string studentId);
        public Task EditExerciseMarkAsync(string exerciseId,double mark,string studentId);
    }
}

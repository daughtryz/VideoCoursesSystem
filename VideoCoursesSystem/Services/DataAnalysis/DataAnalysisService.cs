using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Areas.Services.Teachers;
using VideoCoursesSystem.Data.Models;

namespace VideoCoursesSystem.Services.DataAnalysis
{
    public class DataAnalysisService : IDataAnalysisService
    {
        private readonly IStudentsService _studentsService;
        private readonly ICoursesService _coursesService;

        public DataAnalysisService(IStudentsService studentsService,ICoursesService coursesService)
        {
            _studentsService = studentsService;
            _coursesService = coursesService;
        }
      
        public Dictionary<int, int> GetFrequency()
        {
            var result = new Dictionary<int, int>();
            IEnumerable<Exercise> allExercises = _studentsService.AllExercises();
            IEnumerable<Course> courses = _coursesService.AllCourses();
            int counter = 0;
            foreach (var course in courses)
            {
                var allFilteredExercises = allExercises.Where(e => e.CourseId == course.Id && e.StudentId != null).ToList();
                counter++;
                if(!result.ContainsKey(counter))
                {
                    result.Add(counter, allFilteredExercises.Count);
                }
            }
            var a = 5;
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Areas.Services.Teachers;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.ViewModels.DataAnalysis;

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
            return result;
        }
        
        public List<TendencyViewModel> GetTendency()
        {
            var result = new List<TendencyViewModel>();
            IEnumerable<Course> courses = _coursesService.AllCourses();
            IEnumerable<Exercise> allExercises = _studentsService.AllExercises();

            foreach (var course in courses)
            {
                var exercises = allExercises.Where(e => e.CourseId == course.Id && e.StudentId != null).ToList();

                double sum = 0;
                if(exercises == null)
                {
                    continue;

                }
                if (exercises.Count == 0)
                {
                    continue;
                }
                int count = 0;
                double average = 0.0;
                List<double> grades = new List<double>();
                foreach (var exercise in exercises)
                {
                    if(exercise.Mark == 0)
                    {
                        continue;
                    }
                    count++;
                    sum += exercise.Mark;
                    grades.Add(exercise.Mark);
                }
                average = sum / count;
                if(average == 0)
                {
                    continue;
                }
                double mediana = GetMediana(grades);
                double moda = GetModa(grades);

                result.Add(new TendencyViewModel
                {
                    CourseId = course.Id,
                    Average = average,
                    Mediana = mediana,
                    Moda = moda,
                    CourseTitle = course.Title
                });
                grades.Clear();
            }
            return result;
        }

        private double GetModa(List<double> grades)
        {
            if(grades.Count > 0)
            {
                double max_count = 1, res = grades[0];
                int curr_count = 1;

                for (int i = 1; i < grades.Count; i++)
                {
                    if (grades[i] == grades[i - 1])
                        curr_count++;
                    else
                    {
                        if (curr_count > max_count)
                        {
                            max_count = curr_count;
                            res = grades[i - 1];
                        }
                        curr_count = 1;
                    }
                }

                if (curr_count > max_count)
                {
                    max_count = curr_count;
                    res = grades[grades.Count - 1];
                }
                return res;
            }
           

            return 0.0;
        }

        private double GetMediana(List<double> grades)
        {
            var filteredGrades = grades.OrderBy(g => g).ToList();
            if(filteredGrades.Count == 0)
            {
                return 0.0;
            }
            return filteredGrades[filteredGrades.Count / 2];
        }

        private double GetStandardDeviation(List<double> grades)
        {
            double average = grades.Average();
            double sumOfDerivation = 0;
            foreach (double value in grades)
            {
                sumOfDerivation += (value) * (value);
            }
            double sumOfDerivationAverage = sumOfDerivation / (grades.Count);
            return Math.Sqrt(sumOfDerivationAverage - (average * average));
        }

        public List<DistractionViewModel> GetDistraction()
        {
            var result = new List<DistractionViewModel>();
            IEnumerable<Course> courses = _coursesService.AllCourses();
            IEnumerable<Exercise> allExercises = _studentsService.AllExercises();

            foreach (var course in courses)
            {
                var exercises = allExercises.Where(e => e.CourseId == course.Id && e.StudentId != null).ToList();

                if (exercises == null)
                {
                    continue;

                }
                if (exercises.Count == 0)
                {
                    continue;
                }
                int count = 0;
                List<double> grades = new List<double>();
                foreach (var exercise in exercises)
                {
                    if (exercise.Mark == 0)
                    {
                        continue;
                    }
                    count++;
                    grades.Add(exercise.Mark);
                }
                if(grades.Count == 0)
                {
                    continue;
                }
                double[] scope = GetScope(grades);
                double standardDeviation = GetStandardDeviation(grades);
                result.Add(new DistractionViewModel
                {
                    CourseId = course.Id,
                    Scope = scope[1] - scope[0],
                    CourseTitle = course.Title,
                    StandardDeviation = standardDeviation,
                    Dispersion = Math.Pow(standardDeviation,2)
                });
                grades.Clear();
            }
            return result;
        }

        private double[] GetScope(List<double> grades)
        {
            if(grades.Count > 1)
            {
                var minValue = grades.Min();
                var maxValue = grades.Max();

                return new[] { minValue, maxValue };
            } else if(grades.Count == 1)
            {
                return new[] { 0, grades[0] };
            }

            return new[] { 0.0, 0.0 };
            
        }
    }
}

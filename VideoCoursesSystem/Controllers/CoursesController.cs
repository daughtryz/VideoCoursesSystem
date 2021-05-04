using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Areas.Services.Teachers;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.ViewModels.Courses;
using VideoCoursesSystem.ViewModels.Exercises;

namespace VideoCoursesSystem.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICoursesService _coursesService;
        private readonly IMapper _mapper;
        private readonly IStudentsService _studentsService;

        public CoursesController(ICoursesService coursesService, IMapper mapper,IStudentsService studentsService)
        {
            _coursesService = coursesService;
            _mapper = mapper;
            _studentsService = studentsService;
        }

        public IActionResult All()
        {
            CourseListViewModel courseViewModel = new CourseListViewModel
            {
                Courses = _mapper.Map<IEnumerable<CourseViewModel>>(_coursesService.AllCourses())
            };

            return this.View(courseViewModel);
        }

        public IActionResult Details(string id)
        {
            var viewModel = _mapper.Map<CourseViewModel>(_coursesService.CourseById(id));
            
            string courseId = viewModel.Id;
            foreach (Exercise exercise in _studentsService.AllExercises())
            {
                if (courseId == exercise.CourseId)
                {
                    ExerciseViewModel exerciseViewModel = _mapper.Map<ExerciseViewModel>(exercise);
                    viewModel.Exercises.Add(exerciseViewModel);
                }
            }           
            return this.View(viewModel);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Areas.Services.Teachers;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.Services;
using VideoCoursesSystem.ViewModels.Courses;
using VideoCoursesSystem.ViewModels.Exercises;

namespace VideoCoursesSystem.Controllers
{
    public class CoursesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogsInformationService _logsInformationService;
        private readonly ICoursesService _coursesService;
        private readonly IMapper _mapper;
        private readonly IStudentsService _studentsService;

        public CoursesController(UserManager<ApplicationUser> userManager, ILogsInformationService logsInformationService,ICoursesService coursesService, IMapper mapper,IStudentsService studentsService)
        {
            _userManager = userManager;
            _logsInformationService = logsInformationService;
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

        public async Task<IActionResult> Details(string id)
        {
            var viewModel = _mapper.Map<CourseViewModel>(_coursesService.CourseById(id));
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            string courseId = viewModel.Id;
            foreach (Exercise exercise in _studentsService.AllExercises())
            {
                if (courseId == exercise.CourseId)
                {
                    ExerciseViewModel exerciseViewModel = _mapper.Map<ExerciseViewModel>(exercise);
                    viewModel.Exercises.Add(exerciseViewModel);
                }
            }
            if(applicationUser != null)
            {
                var log = await _logsInformationService.CreateLogAsync($"The user with id '{applicationUser.Id}' viewed the course with id '{courseId}'.");
                await _logsInformationService.CreateUserLogAsync(log.Id, applicationUser.Id);
            }         

            return this.View(viewModel);
        }
    }
}

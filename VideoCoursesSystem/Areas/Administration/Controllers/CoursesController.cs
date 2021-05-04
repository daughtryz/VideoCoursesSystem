using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Areas.Services.Teachers;
using VideoCoursesSystem.InputModels;

namespace VideoCoursesSystem.Areas.Administration.Controllers
{
    public class CoursesController : AdminController
    {
        private readonly ICoursesService _coursesService;
        public CoursesController(ICoursesService teachersService)
        {
            _coursesService = teachersService;
        }
        public IActionResult UploadCourse()
        {
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadCourse(CourseInputModel exercise)
        {
            await _coursesService.CreateCourseAsync(exercise.Title, exercise.Component, exercise.Description, exercise.StartDate, exercise.EndDate);
            return RedirectToAction("All", "Courses", new { area = "" });
        }
    }
}

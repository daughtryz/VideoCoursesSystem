using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.Services;
using VideoCoursesSystem.ViewModels.Courses;

namespace VideoCoursesSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentsService _studentsService;
        private readonly IMapper _mapper;
        private readonly ILogsInformationService _logsInformationService;

        public StudentsController(IStudentsService studentsService,IMapper mapper,ILogsInformationService logsInformationService)
        {
            _studentsService = studentsService;
            _mapper = mapper;
            _logsInformationService = logsInformationService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FirstYearStudentsGrades()
        {
            var viewModel = new GradeListViewModel
            {
                Grades = _mapper.Map<IEnumerable<GradeViewModel>>(_studentsService.GetStudentGrades().Where(x => !x.IsSecondYear).ToList())
            };
            return View(viewModel);
        }

        public IActionResult SecondYearStudentsGrades()
        {
            var viewModel = new GradeListViewModel
            {
                Grades = _mapper.Map<IEnumerable<GradeViewModel>>(_studentsService.GetStudentGrades().Where(x => x.IsSecondYear).ToList())
            };
            return View(viewModel);
        }
        public IActionResult UploadFiles()
        {
            return this.View();
        }

        public IActionResult FileSubmissions()
        {
            return this.View();
        }
        public IActionResult StudentsActivities()
        {
            var logs = new Dictionary<string, List<LogInformationViewModel>>();
            var partLogs = _logsInformationService.GetAllUserLogs().Take(30);
            foreach (var userLog in partLogs)
            {
                string studentId = userLog.StudentId;
                if (!logs.ContainsKey(userLog.StudentId))
                {
                    logs.Add(studentId, new List<LogInformationViewModel>());
                }
                logs[studentId].Add(_mapper.Map<LogInformationViewModel>(_logsInformationService.GetAllLogs().FirstOrDefault(l => l.Id == userLog.LogInformationId)));
            }

            var viewModel = new LogInformationListViewModel
            {
                Logs = logs
            };

            return View(viewModel);
        }
    }
}

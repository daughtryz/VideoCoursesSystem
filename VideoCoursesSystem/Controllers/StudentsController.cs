using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Areas.Services.Teachers;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.InputModels;
using VideoCoursesSystem.Services;
using VideoCoursesSystem.Services.Grades;
using VideoCoursesSystem.Services.Helpers;
using VideoCoursesSystem.ViewModels.Courses;
using VideoCoursesSystem.ViewModels.Exercises;

namespace VideoCoursesSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IHelperService _helperService;
        private readonly IStudentsService _studentsService;
        private readonly IMapper _mapper;
        private readonly ILogsInformationService _logsInformationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGradesService _gradesService;
        private readonly ICoursesService _coursesService;

        public StudentsController(IHelperService helperService,
            IStudentsService studentsService,
            IMapper mapper,
            ILogsInformationService logsInformationService,
            UserManager<ApplicationUser> userManager,
            IGradesService gradesService,
            ICoursesService coursesService)
        {
            _helperService = helperService;
            _studentsService = studentsService;
            _mapper = mapper;
            _logsInformationService = logsInformationService;
            _userManager = userManager;
            _gradesService = gradesService;
            _coursesService = coursesService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public string CourseId { get; set; }
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
        [Authorize]
        public IActionResult UploadExercise(string courseId)
        {
            _helperService.AddCourseId(courseId);
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadExercise(IEnumerable<IFormFile> exercises)
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
            string courseId = _helperService.GetCourseId();
            await _studentsService.UploadExerciseAsync(exercises, applicationUser.Id, courseId);
            return RedirectToAction("Details", "Courses", new { area = "",id = courseId });
        }
        public IActionResult FileSubmissions()
        {
            return this.View();
        }
        [Authorize(Roles = "Admin")]
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
        public IActionResult ExerciseEdit(string id)
        {
            var viewModel = _mapper.Map<ExerciseViewModel>(_studentsService.GetExerciseById(id));
            if (_studentsService.GetExerciseById(id).FileName.Contains("|"))
            {
                viewModel.FileNames = _studentsService.GetExerciseById(id).FileName.Split("|").ToArray();
            }
            else
            {
                viewModel.FileName = _studentsService.GetExerciseById(id).FileName;
            }

            return this.View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ExerciseEdit(string id, IEnumerable<IFormFile> exercises)
        {
            var viewModel = _mapper.Map<ExerciseViewModel>(_studentsService.GetExerciseById(id));
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);    
            await _studentsService.EditExerciseAsync(exercises, id);

            return RedirectToAction("Details", "Courses", new { area = "", id = viewModel.CourseId });
        }
        public IActionResult ExerciseEditMark(string exerciseId)
        {
            _helperService.AddExerciseId(exerciseId);
            return this.View();
        }
        [HttpPost]
        public async Task<IActionResult> ExerciseEditMark(ExerciseViewModel model)
        {
            var exerciseId = _helperService.GetExerciseId();
            var curr = _studentsService.GetExerciseById(exerciseId);
            var viewModel = _mapper.Map<ExerciseViewModel>(_studentsService.GetExerciseById(exerciseId));
           
            await _studentsService.EditExerciseMarkAsync(viewModel.Id, model.Mark,viewModel.StudentId);
            return this.RedirectToAction("ExerciseDetails","Students",new {id= viewModel.Id });
        }
        public IActionResult ExerciseDetails(string id)
        {
            var viewModel = _mapper.Map<ExerciseViewModel>(_studentsService.GetExerciseById(id));
            if (_studentsService.GetExerciseById(id).FileName.Contains("|"))
            {
                viewModel.FileNames = _studentsService.GetExerciseById(id).FileName.Split("|").ToArray();
            } else
            {
                viewModel.FileName = _studentsService.GetExerciseById(id).FileName;
            }
            
            return this.View(viewModel);
        }
        [Authorize]
        public async Task<IActionResult> MyExercises()
        {
            var user = await _userManager.FindByIdAsync(User.Identity.Name);
           
            var viewModel = new ExerciseListViewModel
            {
                Exercises = _studentsService.GetStudentExercises(user.Id).Select(se => new ExerciseViewModel
                {
                    Mark = se.Mark,
                    Course = _mapper.Map<CourseViewModel>(_coursesService.CourseById(se.CourseId))
                })
            };
            return this.View(viewModel);
        }
        public async Task<IActionResult> DownloadExercise(string fileName)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".rar","application/x-rar-compressed" },
                {".zip","application/zip" }
            };
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
    }
}

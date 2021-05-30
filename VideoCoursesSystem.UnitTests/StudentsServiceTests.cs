using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoCoursesSystem.Areas.Services.Teachers;
using VideoCoursesSystem.Data;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.Services;
using VideoCoursesSystem.Services.Grades;
using Xunit;

namespace VideoCoursesSystem.UnitTests
{
    public class StudentsServiceTests
    {
        private StudentsService _studentsService;
        private CoursesService _coursesService;
        public StudentsServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "VideoCoursesSystem");
            _studentsService = new StudentsService(new ApplicationDbContext(options.Options),new GradesService(new ApplicationDbContext(options.Options)));
            _coursesService = new CoursesService(new ApplicationDbContext(options.Options));
        }

        [Fact]
        public async Task ItShouldAssessTheStudentsMark()
        {
            var student = new ApplicationUser
            {
                Id = "123",
                UserName = "Ivan"
            };
            List<IFormFile> files = new List<IFormFile>();
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            files.Add(file);
            await _coursesService.CreateCourseAsync("TestTitle", "TestComponent", "TestDescription", new DateTime(2015, 12, 30), new DateTime(2015, 12, 31));
            var currentCourse = _coursesService.AllCourses().FirstOrDefault();
            await _studentsService.UploadExerciseAsync(files,student.Id, currentCourse.Id);
            var currentExercise = _studentsService.AllExercises().FirstOrDefault();
            currentExercise.Mark = 5;
            int expectedMark = 5;
            Assert.Equal(expectedMark, currentExercise.Mark);
        }
        [Fact]
        public async Task ItShouldAccessMyMarks()
        {
            var student = new ApplicationUser
            {
                Id = "111",
                UserName = "unknown",
            };
            List<IFormFile> files = new List<IFormFile>();
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            files.Add(file);
            await _coursesService.CreateCourseAsync("TestTitle", "TestComponent", "TestDescription", new DateTime(2015, 12, 30), new DateTime(2015, 12, 31));
            var currentCourse = _coursesService.AllCourses().FirstOrDefault();
            await _studentsService.UploadExerciseAsync(files, student.Id, currentCourse.Id);
            var currentExercise = _studentsService.AllExercises().FirstOrDefault(x => x.StudentId == student.Id);
            currentExercise.Mark = 6;
            Assert.NotNull(currentExercise);
        }
        [Fact]
        public async Task ItShouldShowAllExercisesSubmittedByMe()
        {
            var student = new ApplicationUser
            {
                Id = "222",
                UserName = "Ivan",
            };
            List<IFormFile> files = new List<IFormFile>();
            IFormFile file1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            IFormFile file2 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a second dummy file")), 0, 0, "Data", "dm.txt");

            files.Add(file1);
            files.Add(file2);

            await _coursesService.CreateCourseAsync("TestTitle", "TestComponent", "TestDescription", new DateTime(2015, 12, 30), new DateTime(2015, 12, 31));
            var currentCourse = _coursesService.AllCourses().FirstOrDefault();
            await _studentsService.UploadExerciseAsync(files, student.Id, currentCourse.Id);
         
            var currentExercises = _studentsService.GetSubmittedExercises(student.Id).ToList();
            var expectedCount = 1;
            Assert.Equal(expectedCount, currentExercises.Count);
        }
        
        [Fact]
        public async Task ItShouldUploadExercise()
        {
            var student = new ApplicationUser
            {
                Id = "420",
                UserName = "Zzzz",
            };
            await _coursesService.CreateCourseAsync("Matematika3", "TestComponent", "TestDescription", new DateTime(2015, 12, 30), new DateTime(2015, 12, 31));
            var currentCourse = _coursesService.AllCourses().FirstOrDefault(x => x.Title == "Matematika3");

            List<IFormFile> files = new List<IFormFile>();
            IFormFile file1 = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("This is a dummy file")), 0, 0, "Data", "dummy.txt");
            await _studentsService.UploadExerciseAsync(files, student.Id, currentCourse.Id);
            var expectedCount = 2;
            Assert.Equal(expectedCount, _studentsService.AllExercises().Count());
        }
    }
}

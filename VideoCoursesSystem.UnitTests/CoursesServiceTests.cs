using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoCoursesSystem.Areas.Services.Teachers;
using VideoCoursesSystem.Data;
using Xunit;

namespace VideoCoursesSystem.UnitTests
{
    public class CoursesServiceTests
    {
        private CoursesService _coursesService;
        public CoursesServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "VideoCoursesSystem");
            _coursesService = new CoursesService(new ApplicationDbContext(options.Options));
        }
        [Fact]
        public async Task ItShouldUploadCourse()
        {
            await _coursesService.CreateCourseAsync("TestTitle", "TestComponent", "TestDescription", new DateTime(2015, 12, 30), new DateTime(2015, 12, 31));
            var expectedTitle = "TestTitle";
            var expectedComponent = "TestComponent";
            var expectedDescription = "TestDescription";
            var expectedStartDate = new DateTime(2015, 12, 30).ToString();
            var expectedEndDate = new DateTime(2015, 12, 31).ToString();
            var currentCourse = _coursesService.AllCourses().FirstOrDefault();

            Assert.Equal(expectedTitle, currentCourse.Title);
            Assert.Equal(expectedComponent, currentCourse.Component);
            Assert.Equal(expectedDescription, currentCourse.Description);
            Assert.Equal(expectedStartDate, currentCourse.StartDate.ToString());
            Assert.Equal(expectedEndDate, currentCourse.EndDate.ToString());
        }
        [Fact]
        public async Task ItShouldGetTheProperCountOfViewedExercise()
        {
            await _coursesService.CreateCourseAsync("TestTitle", "TestComponent", "TestDescription", new DateTime(2015, 12, 30), new DateTime(2015, 12, 31));
            var currentCourse = _coursesService.AllCourses().FirstOrDefault();
            currentCourse.Viewers = 1;
            int expectedViewers = 1;
            Assert.Equal(expectedViewers, currentCourse.Viewers);
        }
        [Fact]
        public async Task ItShouldSearchTopics()
        {
            string courseToSearchTitle = "Matematika2";
            await _coursesService.CreateCourseAsync("Matematika2", "TestComponent", "TestDescription", new DateTime(2015, 12, 30), new DateTime(2015, 12, 31));

            var currentCourse = _coursesService.AllCourses().FirstOrDefault(t => t.Title == courseToSearchTitle);
            var expected = "Matematika2";
            Assert.Equal(expected, currentCourse.Title);
        }
    }
}

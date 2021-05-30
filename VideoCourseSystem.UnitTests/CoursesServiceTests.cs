using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace VideoCourseSystem.UnitTests
{
    public class CoursesServiceTests
    {
        private LogsInformationService _logInformationService;
        public CoursesServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "VideoCoursesSystem");
            _logInformationService = new LogsInformationService(new ApplicationDbContext(options.Options));
        }
        [Fact]
        public void ItShouldUploadCourse()
        {

        }
    }
}

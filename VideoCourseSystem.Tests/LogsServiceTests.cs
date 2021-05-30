using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using VideoCoursesSystem.Data;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.Services;
using Xunit;

namespace VideoCourseSystem.Tests
{
    public class LogsServiceTests
    {
        private LogsInformationService _logsInformationService;
        DbContextOptionsBuilder<ApplicationDbContext> _optionsBuilder;
        public LogsServiceTests()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            _logsInformationService = new LogsInformationService(new ApplicationDbContext(options.Options));
        }
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "VideoCoursesSystem")
            .Options;
        }
    }
}

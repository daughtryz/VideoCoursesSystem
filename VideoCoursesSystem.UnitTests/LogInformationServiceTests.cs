using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoCoursesSystem.Data;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.Services;
using Xunit;

namespace VideoCoursesSystem.UnitTests
{
    public class LogInformationServiceTests
    {
        private LogsInformationService _logInformationService;
        public LogInformationServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "VideoCoursesSystem");
            _logInformationService = new LogsInformationService(new ApplicationDbContext(options.Options));
        }
        [Fact]
        public async Task ItShouldAccessTheLogs()
        {
            var appUser = new ApplicationUser
            {
                Id = "123",
                UserName = "Test"
            };
            var log = await _logInformationService.CreateLogAsync($"The user with id '{appUser.Id}' viewed the course with id '111'.");
            await _logInformationService.CreateUserLogAsync(log.Id, appUser.Id);
            string expectedDescription = "The user with id '123' viewed the course with id '111'.";
            var currentLog = _logInformationService.GetAllLogs().FirstOrDefault(l => l.Id == log.Id);
            Assert.Equal(expectedDescription, currentLog.Description);
        }
    }
}

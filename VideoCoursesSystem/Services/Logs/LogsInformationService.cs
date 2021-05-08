using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data;
using VideoCoursesSystem.Data.Models;

namespace VideoCoursesSystem.Services
{
    public class LogsInformationService : ILogsInformationService
    {
        private readonly ApplicationDbContext _db;

        public LogsInformationService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<LogInformation> CreateLogAsync(string description)
        {
            var log = new LogInformation
            {
                Description = description,
                CreatedOn = DateTime.UtcNow
            };
            await _db.LogsInformation.AddAsync(log);
            await _db.SaveChangesAsync();
            return log;
        }

        public async Task CreateUserLogAsync(string logInfoId, string userId)
        {
            var userLog = new UserLogInformation
            {
                LogInformationId = logInfoId,
                StudentId = userId
            };

            await _db.UserLogsInformation.AddAsync(userLog);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<LogInformation> GetAllLogs()
        {
            return _db.LogsInformation.OrderByDescending(l => l.CreatedOn).ToList();
        }

        public IEnumerable<UserLogInformation> GetAllUserLogs()
        {
            return _db.UserLogsInformation.ToList();
        }
    }
}

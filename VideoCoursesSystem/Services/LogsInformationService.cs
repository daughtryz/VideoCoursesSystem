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
        public IEnumerable<LogInformation> GetAllLogs()
        {
            return _db.LogsInformation.ToList();
        }

        public IEnumerable<UserLogInformation> GetAllUserLogs()
        {
            return _db.UserLogsInformation.ToList();
        }
    }
}

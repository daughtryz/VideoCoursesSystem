using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data.Models;

namespace VideoCoursesSystem.Services
{
    public interface ILogsInformationService
    {
        public IEnumerable<LogInformation> GetAllLogs();

        public IEnumerable<UserLogInformation> GetAllUserLogs();

    }
}

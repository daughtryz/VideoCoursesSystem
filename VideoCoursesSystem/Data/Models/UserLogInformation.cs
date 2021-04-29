using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.Data.Models
{
    public class UserLogInformation
    {
        public string LogInformationId { get; set; }
        public LogInformation LogInformation { get; set; }
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data.Models.Enums;

namespace VideoCoursesSystem.Data.Models
{
    public class Course
    {
        public Course()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime Time { get; set; }
        public string EventContext { get; set; }

        public string Component { get; set; }

        public string EventName { get; set; }
        public PlatformType PlatformType { get; set; }

        public string Description { get; set; }
    }
}

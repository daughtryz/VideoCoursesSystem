using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.Services.Helpers
{
    public class HelperService : IHelperService, IDisposable
    {
        public HelperService()
        {
            this.Ids = new List<string>();
        }
        public List<string> Ids { get; set; }
        public void AddCourseId(string courseId)
        {
            this.Ids.Add(courseId);
        }
        public void Dispose()
        {
            this.Ids.Clear();
        }

        public string GetCourseId()
        {
            var currentId = this.Ids.FirstOrDefault();
            return currentId;
        }
    }
}

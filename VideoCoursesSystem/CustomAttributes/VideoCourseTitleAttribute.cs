using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.CustomAttributes
{
    [System.AttributeUsage(System.AttributeTargets.Class |
                       System.AttributeTargets.Struct,
                       AllowMultiple = true)]
    public class VideoCourseTitleAttribute : Attribute
    {      
        public VideoCourseTitleAttribute(string title)
        {
            this.Title = title;
        }
        public string Title { get; set; }
        public string TitleVersion { get; set; }
    }
}

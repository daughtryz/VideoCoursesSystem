using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.CustomAttributes
{
    public class VideoCoursesCategoriesAttribute : Attribute
    {
        public VideoCoursesCategoriesAttribute(string[] categories)
        {
            Categories = categories;
        }
        public string[] Categories { get; set; }
    }
}

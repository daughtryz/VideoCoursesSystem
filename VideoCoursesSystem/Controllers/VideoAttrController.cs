using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.CustomAttributes;

namespace VideoCoursesSystem.Controllers
{
    [VideoCourseTitle("VideoTitleController", TitleVersion = "OldVersion")]
    public class VideoAttrController : Controller
    {
        public VideoAttrController()
        {

        }
        public IActionResult VideoRes()
        {
            VideoCourseTitleAttribute attr = (VideoCourseTitleAttribute)Attribute.GetCustomAttribute(typeof(VideoAttrController),typeof(VideoCourseTitleAttribute));

            if(attr == null)
            {
                throw new ArgumentException();
            }

            VideoAttributeResult result = new VideoAttributeResult
            {
                Title = attr.Title,
                TitleVersion = attr.TitleVersion
            };

            return this.View(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VideoCoursesSystem.Data.Models
{
    public class UserCourse
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }

        public DateTime Time { get; set; }
        public string EventContext { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Title { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string Component { get; set; }

        public string EventName { get; set; }
        public PlatformType PlatformType { get; set; }

        [Required]
        public string Description { get; set; }
        public int Viewers { get; set; }
        public ICollection<Exercise> Exercises { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.ViewModels.Exercises
{
    public class ExerciseViewModel
    {
        public string Id { get; set; }
        public double Mark { get; set; }
        public byte[] File { get; set; }
        public string CourseId { get; set; }
        public string StudentId { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime DateSubmission { get; set; }
        public string[] FileNames { get; set; }
        public string FileName { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.Data.Models
{
    public class Exercise
    {
        public Exercise()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public double Mark { get; set; }
        public byte[] File { get; set; }
        public int EditWikiCount { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime DateSubmission { get; set; }
        public string FileName { get; set; }
        public string GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}

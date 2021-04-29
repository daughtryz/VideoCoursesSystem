using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.Data.Models
{
    public class Grade
    {
        public Grade()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }

        public bool IsSecondYear { get; set; }
        public double Mark { get; set; }

        // Hello World

        public string StudentId { get; set; }
        public ApplicationUser Student { get; set; }

    }
}

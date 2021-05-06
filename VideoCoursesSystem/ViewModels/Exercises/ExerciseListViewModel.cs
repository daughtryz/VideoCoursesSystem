using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.ViewModels.Exercises
{
    public class ExerciseListViewModel
    {
        public IEnumerable<ExerciseViewModel> Exercises { get; set; }
    }
}

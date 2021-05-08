using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.ViewModels.DataAnalysis
{
    public class DistractionViewModel
    {
        public double StandardDeviation { get; set; }
        public double Dispersion { get; set; }
        public double Scope { get; set; }
        public string CourseTitle { get; set; }
        public string CourseId { get; set; }
    }
}

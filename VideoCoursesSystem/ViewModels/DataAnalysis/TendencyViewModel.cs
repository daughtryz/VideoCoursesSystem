using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.ViewModels.DataAnalysis
{
    public class TendencyViewModel
    {
        public string CourseTitle { get; set; }
        public double Average { get; set; }

        public double Mediana { get; set; }

        public double Moda { get; set; }
    }
}

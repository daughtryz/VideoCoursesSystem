using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.ViewModels.DataAnalysis
{
    public class FrequencyViewModel
    {
        public Dictionary<int, int> Freq { get; set; }
        public int AllCount { get; set; }
        public double OtnFrequency { get; set; }
        public List<double> FrequenciesOtn { get; set; }
    }
}

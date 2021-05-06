using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.Services.DataAnalysis
{
    public interface IDataAnalysisService
    {
        Dictionary<int, int> GetFrequency();
    }
}

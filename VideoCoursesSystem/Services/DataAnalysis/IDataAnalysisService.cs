using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.ViewModels.DataAnalysis;

namespace VideoCoursesSystem.Services.DataAnalysis
{
    public interface IDataAnalysisService
    {
        Dictionary<int, int> GetFrequency();
        List<TendencyViewModel> GetTendency();

        List<DistractionViewModel> GetDistraction();
    }
}

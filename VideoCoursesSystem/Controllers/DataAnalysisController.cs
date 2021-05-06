using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Services.DataAnalysis;
using VideoCoursesSystem.ViewModels.DataAnalysis;

namespace VideoCoursesSystem.Controllers
{
    public class DataAnalysisController : Controller
    {
        private readonly IDataAnalysisService _dataAnalysisService;

        public DataAnalysisController(IDataAnalysisService dataAnalysisService)
        {
            _dataAnalysisService = dataAnalysisService;
        }

        public IActionResult Frequency()
        {
            var result = _dataAnalysisService.GetFrequency();
            FrequencyViewModel viewModel = new FrequencyViewModel
            {
                Freq = result,
                AllCount = result.Values.Sum(),
                FrequenciesOtn = new List<double>()
            };

            foreach (var kvp in result)
            {
                var res = Math.Round((kvp.Value * 1.0 / viewModel.AllCount * 100), 1);
                viewModel.OtnFrequency += res;
                viewModel.FrequenciesOtn.Add(res);
            }

            return this.View(viewModel);
        }

    }
}

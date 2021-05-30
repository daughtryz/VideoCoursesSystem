using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoCoursesSystem.Areas.Services.Teachers;
using VideoCoursesSystem.Data;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.Services;
using VideoCoursesSystem.Services.DataAnalysis;
using VideoCoursesSystem.Services.Grades;
using VideoCoursesSystem.ViewModels.DataAnalysis;
using Xunit;
using ZendeskApi_v2.Models.CustomRoles;

namespace VideoCoursesSystem.UnitTests
{
    public class DataAnalysisServiceTests
    {
        private DataAnalysisService _dataAnalysisService;
        private StudentsService _studentsService;
        private CoursesService _coursesService;
        public DataAnalysisServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "VideoCoursesSystem");
            _studentsService = new StudentsService(new ApplicationDbContext(options.Options), new GradesService(new ApplicationDbContext(options.Options)));
            _coursesService = new CoursesService(new ApplicationDbContext(options.Options));
            var appUser = new ApplicationUser
            {
                Id = "111",
                UserName = "Test"
            };
            
            _dataAnalysisService = new DataAnalysisService(_studentsService, _coursesService);
        }
        [Fact]
        public void ItShouldCalculateProperlyFrequency()
        {
            var result = new Dictionary<int, int>();
            result[1] = 2;
            result[2] = 3;
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
            };
            var expectedFrequency = 100;
            Assert.Equal(expectedFrequency, viewModel.OtnFrequency);
        }
        [Fact]
        public void ItShouldCalculateProperlyTendency()
        {
            List<double> grades = new List<double>
            {
                5,5,6
            };

            TendencyViewModel vm = new TendencyViewModel
            {
                Moda = _dataAnalysisService.GetModa(grades),
                Mediana = _dataAnalysisService.GetMediana(grades),
                Average = Math.Round(grades.Sum() / grades.Count,2)
            };

            var expectedModa = 5;
            var expectedMediana = 5;
            var expectedAverage = 5.33;
            Assert.Equal(expectedModa, vm.Moda);
            Assert.Equal(expectedMediana, vm.Mediana);
            Assert.Equal(expectedAverage, vm.Average);
        }

        [Fact]
        public void ItShouldCalculateProperlyDistraction()
        {
            List<double> grades = new List<double>
            {
                5,5,6
            };
            var standardDeviation = _dataAnalysisService.GetStandardDeviation(grades);
            double[] scope = _dataAnalysisService.GetScope(grades);
            DistractionViewModel vm = new DistractionViewModel
            {
                StandardDeviation = standardDeviation,
                Dispersion = Math.Pow(standardDeviation, 2),
                Scope = scope[1] - scope[0]
            };

            var expectedStandardDeviation = 0.47;
            var expectedDispersion = 0.22;
            var expectedScope = 1;
            Assert.Equal(expectedStandardDeviation, Math.Round(vm.StandardDeviation,2));
            Assert.Equal(expectedDispersion, Math.Round(vm.Dispersion,2));
            Assert.Equal(expectedScope, vm.Scope);
        }
        [Fact]
        public void ItShouldCalculateProperlyEdittedWikiFrequency()
        {
            var result = new Dictionary<string, int>();
            result["Kurs1"] = 2;
            result["Kurs2"] = 3;
            FrequencyViewModel viewModel = new FrequencyViewModel
            {
                FreqWiki = result,
                AllCount = result.Values.Sum(),
                FrequenciesOtn = new List<double>()
            };

            foreach (var kvp in result)
            {
                var res = Math.Round((kvp.Value * 1.0 / viewModel.AllCount * 100), 1);
                viewModel.OtnFrequency += res;
                viewModel.FrequenciesOtn.Add(res);
            };
            var expectedFrequency = 100;
            Assert.Equal(expectedFrequency, viewModel.OtnFrequency);
        }
    }
}

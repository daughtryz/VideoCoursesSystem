using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.Models;

namespace VideoCoursesSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            //Index page.
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> UploadStudentsGrades(IFormFile uploadedFile)
        {

            using (var stream = new MemoryStream())
            {
                await uploadedFile.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;
                    for (int i = 2; i <= rowCount; i++)
                    {
                        //string studentId = worksheet.Cells[i, 1].Value.ToString();
                        //double mark = double.Parse(worksheet.Cells[i, 2].Value.ToString());
                        string description = worksheet.Cells[i, 5].Value.ToString();
                        string foundStudentId = description.Split(" ", StringSplitOptions.RemoveEmptyEntries).FirstOrDefault(c => c.Contains('\''));
                        string studentId = foundStudentId.Substring(1, foundStudentId.Length - 2);
                        LogInformation logInformation = new LogInformation
                        {                           
                            Description = description
                        };
                        
                        //Grade grade = new Grade
                        //{
                        //    StudentId = studentId,
                        //    Mark = mark,
                        //    IsSecondYear = true

                        //};
                        //_db.Users.Add(new ApplicationUser
                        //{
                        //    Id = studentId,
                        //});

                        _db.LogsInformation.Add(logInformation);
                        _db.UserLogsInformation.Add(new UserLogInformation
                        {
                            StudentId = studentId,
                            LogInformationId = logInformation.Id
                        });
                        _db.SaveChanges();
                    }
                }
            }
            return Ok();
        }
    }
}

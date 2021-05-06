using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data.Models;

namespace VideoCoursesSystem.Services.Grades
{
    public interface IGradesService
    {
        Task<Grade> CreateGradeAsync(double mark,string studentId);
        Grade GetStudentGradeById(string gradeId, string studentId);
        Task UpdateGradeAsync(double newMark, string studentId, string gradeId);
    }
}

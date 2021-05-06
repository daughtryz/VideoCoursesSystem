using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data;
using VideoCoursesSystem.Data.Models;

namespace VideoCoursesSystem.Services.Grades
{
    public class GradesService : IGradesService
    {
        private readonly ApplicationDbContext _db;

        public GradesService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Grade> CreateGradeAsync(double mark,string studentId)
        {
            if(mark < 0 || mark > 6)
            {
                throw new ArgumentException("Invalid mark!");
            }
            Grade grade = new Grade
            {
                Mark = mark,
                StudentId = studentId
            };
            await _db.Grades.AddAsync(grade);
            await _db.SaveChangesAsync();
            return grade;
        }
        public Grade GetStudentGradeById(string gradeId,string studentId) => _db.Grades.FirstOrDefault(g => g.Id == gradeId && g.StudentId == studentId);
        public async Task UpdateGradeAsync(double newMark, string studentId, string gradeId)
        {
            var studentGrade = GetStudentGradeById(gradeId, studentId);

            if (studentGrade == null)
            {
                await CreateGradeAsync(newMark, studentId);
            } else
            {
                studentGrade.Mark = newMark;
                _db.Grades.Update(studentGrade);
            }
            await _db.SaveChangesAsync();
        }
    }
}

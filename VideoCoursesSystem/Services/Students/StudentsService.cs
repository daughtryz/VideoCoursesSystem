using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.Services.Grades;
using VideoCoursesSystem.ViewModels.Courses;

namespace VideoCoursesSystem.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly ApplicationDbContext _db;
        private readonly IGradesService _gradesService;
        private Dictionary<string, List<string>> _helper;
        public StudentsService(ApplicationDbContext db,IGradesService gradesService)
        {
            _db = db;
            _gradesService = gradesService;
            _helper = new Dictionary<string, List<string>>();
        }

        public IEnumerable<Exercise> AllExercises()
        {
            return _db.Exercises.ToList();
        }

        public async Task EditExerciseAsync(IEnumerable<IFormFile> exercises, string exerciseId)
        {
            Exercise currentExercise = _db.Exercises.FirstOrDefault(e => e.Id == exerciseId);
            if(currentExercise == null)
            {
                throw new ArgumentException("No such exercise!");
            }
            foreach (var exercise in exercises)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await exercise.CopyToAsync(memoryStream);
                    currentExercise.LastModified = DateTime.UtcNow;
                    currentExercise.FileName += "|" + exercise.FileName;
                    _db.Exercises.Update(currentExercise);
                }
            }
            currentExercise.EditWikiCount++;
            _db.Exercises.Update(currentExercise);
            await _db.SaveChangesAsync();
        }
        public async Task EditExerciseMarkAsync(string exerciseId,double mark,string studentId)
        {
            Exercise currentExercise = _db.Exercises.FirstOrDefault(e => e.Id == exerciseId);
            if(currentExercise.GradeId == null)
            {
                Grade currentGrade = await _gradesService.CreateGradeAsync(mark, studentId);
                currentExercise.GradeId = currentGrade.Id;
            } else
            {
                await _gradesService.UpdateGradeAsync(mark, studentId, currentExercise.GradeId);
            }

            if (currentExercise == null)
            {
                throw new ArgumentException("No such exercise!");
            }
            currentExercise.Mark = mark;
            _db.Exercises.Update(currentExercise);
            await _db.SaveChangesAsync();
        }

        public Exercise GetExerciseById(string id)
        {
            return _db.Exercises.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<UserCourse> GetStudentActivities()
        {
            return _db.UserCourses.ToList();
        }

        public IEnumerable<Exercise> GetStudentExercises(string studentId)
        {
            return _db.Exercises.Where(x => x.StudentId == studentId && x.GradeId != null).ToList();            
        }
        public IEnumerable<Exercise> GetSubmittedExercises(string studentId)
        {
            return _db.Exercises.Where(x => x.StudentId == studentId && x.GradeId == null).ToList();
        }
        public IEnumerable<Grade> GetStudentGrades()
        {
            return _db.Grades.ToList();
        }

        public async Task UploadExerciseAsync(IEnumerable<IFormFile> exercises,string studentId,string courseId)
        {
            foreach (var exercise in exercises)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await exercise.CopyToAsync(memoryStream);
                    
                    if(!_db.Exercises.Any(e => e.StudentId == studentId && e.CourseId == courseId))
                    {
                        var exerciseDb = new Exercise
                        {
                            StudentId = studentId,
                            CourseId = courseId,
                            DateSubmission = DateTime.UtcNow,
                            LastModified = DateTime.UtcNow,
                            File = memoryStream.ToArray(),
                            FileName = exercise.FileName
                        };

                        await _db.Exercises.AddAsync(exerciseDb);
                    } else
                    {
                        Exercise existingExerciseByStudent = _db.Exercises.FirstOrDefault(e => e.StudentId == studentId && e.CourseId == courseId);
                        existingExerciseByStudent.FileName += "|" + exercise.FileName;
                    }

                    await _db.SaveChangesAsync();
                }
            }
            await _db.SaveChangesAsync();
        }
    }
}

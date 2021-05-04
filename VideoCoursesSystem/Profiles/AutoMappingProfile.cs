using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoCoursesSystem.Data.Models;
using VideoCoursesSystem.ViewModels.Courses;
using VideoCoursesSystem.ViewModels.Exercises;

namespace VideoCoursesSystem.Profiles
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<LogInformation, LogInformationViewModel>().ReverseMap();
            CreateMap<Grade, GradeViewModel>();
            CreateMap<Course, CourseViewModel>();
            CreateMap<Exercise, ExerciseViewModel>();
        }
    }
}

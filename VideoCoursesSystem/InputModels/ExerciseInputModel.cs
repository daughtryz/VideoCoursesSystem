using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VideoCoursesSystem.InputModels
{
    public class ExerciseInputModel
    {
        [BindProperty]
        public IFormFile File { get; set; }
    }
}

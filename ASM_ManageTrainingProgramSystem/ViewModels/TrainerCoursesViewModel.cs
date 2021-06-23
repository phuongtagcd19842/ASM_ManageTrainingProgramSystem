using ASM_ManageTrainingProgramSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASM_ManageTrainingProgramSystem.ViewModels
{
    public class TrainerCoursesViewModel
    {
        public string UserId { get; set; }
        public int CourseId { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
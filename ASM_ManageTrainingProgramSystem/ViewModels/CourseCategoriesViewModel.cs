using ASM_ManageTrainingProgramSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASM_ManageTrainingProgramSystem.ViewModels
{
    public class CourseCategoriesViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
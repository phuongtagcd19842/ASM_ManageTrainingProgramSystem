using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ASM_ManageTrainingProgramSystem.Models;
using ASM_ManageTrainingProgramSystem.UniqueAttribute;

namespace ASM_ManageTrainingProgramSystem.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        [Unique(ErrorMessage = "Course already exists!")]
        public string CourseName { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } //link object
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASM_ManageTrainingProgramSystem.Models
{
    public class TraineeInfo
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string TraineeName { get; set; }
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Education { get; set; }
        public string ProgrammingLanguage { get; set; }
        public float TOEICScore { get; set; }
        public string ExperienceDetail { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
    }
}
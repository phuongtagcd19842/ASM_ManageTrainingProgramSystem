using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Required]
        [DisplayName("Trainee Name")]
        public string TraineeName { get; set; }
        public int Age { get; set; }
        [DisplayName("Date Of Birth")]
        public string DateOfBirth { get; set; }
        public string Education { get; set; }
        [DisplayName("Programming Language")]
        public string ProgrammingLanguage { get; set; }
        [DisplayName("TOEIC Score")]
        public float TOEICScore { get; set; }
        [DisplayName("Experience Detail")]
        public string ExperienceDetail { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }

    }
}
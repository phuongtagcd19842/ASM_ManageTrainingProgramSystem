using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASM_ManageTrainingProgramSystem.Models
{
    public class TrainerInfo
    {
        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string TrainerName { get; set; }
        public int ExternalOrInternal { get; set; }
        public string WorkingPlace { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }

    }
}
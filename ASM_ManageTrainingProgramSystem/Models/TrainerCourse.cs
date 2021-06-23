using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASM_ManageTrainingProgramSystem.Models
{
    public class TrainerCourse
    {
		[Key]
		[Column(Order = 1)]
		[ForeignKey("TrainerInfo")]
		public string UserId { get; set; }
		public TrainerInfo TrainerInfo { get; set; }

		[Key]
		[Column(Order = 2)]
		[ForeignKey("Course")]
		public int CourseId { get; set; }
		public Course Course { get; set; }
	}
}
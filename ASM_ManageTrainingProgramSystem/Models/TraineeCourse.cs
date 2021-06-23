using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASM_ManageTrainingProgramSystem.Models
{
    public class TraineeCourse
    {
		[Key]
		[Column(Order = 1)]
		[ForeignKey("TraineeInfo")]
		public string UserId { get; set; }
		public TraineeInfo TraineeInfo { get; set; }

		[Key]
		[Column(Order = 2)]
		[ForeignKey("Course")]
		public int CourseId { get; set; }
		public Course Course { get; set; }
	}
}
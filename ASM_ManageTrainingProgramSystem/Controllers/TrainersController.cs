using ASM_ManageTrainingProgramSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASM_ManageTrainingProgramSystem.Controllers
{
	public class TrainersController : Controller
	{
		private ApplicationDbContext _context;
		public TrainersController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: TrainerInfo
		public ActionResult Index()
		{
			var userId = User.Identity.GetUserId();
			var trainerInfo = _context.TrainersInfo.SingleOrDefault(u => u.UserId.Equals(userId));

			if (trainerInfo == null) return HttpNotFound();

			return View(trainerInfo);
		}

		[HttpGet]
		public ActionResult Edit()
		{
			var userId = User.Identity.GetUserId();
			var trainerInfo = _context.TrainersInfo.SingleOrDefault(u => u.UserId.Equals(userId));

			if (trainerInfo == null) return HttpNotFound();

			return View(trainerInfo);
		}

		[HttpPost]
		public ActionResult Edit(TrainerInfo trainerInfo)
		{
			var TrainerInfoInDb = _context.TrainersInfo.SingleOrDefault(u => u.UserId.Equals(trainerInfo.UserId));

			if (trainerInfo == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			TrainerInfoInDb.TrainerName = trainerInfo.TrainerName;
			TrainerInfoInDb.ExternalOrInternal = trainerInfo.ExternalOrInternal;
			TrainerInfoInDb.WorkingPlace = trainerInfo.WorkingPlace;
			TrainerInfoInDb.Telephone = trainerInfo.Telephone;
			TrainerInfoInDb.EmailAddress = trainerInfo.EmailAddress;
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

		public ActionResult ViewAssignedCourse()
        {
			var userId = User.Identity.GetUserId();
			var CourseAssigned = _context.TrainerCourses
				.Where(c => c.UserId.Equals(userId))
				.Include(c => c.Course)
				.Select(c => c.Course)
				.ToList();

			if (CourseAssigned == null) return HttpNotFound();

			return View(CourseAssigned);
        }
	}
}
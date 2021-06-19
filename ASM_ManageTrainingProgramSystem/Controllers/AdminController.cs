﻿using ASM_ManageTrainingProgramSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASM_ManageTrainingProgramSystem.ViewModels;

namespace ASM_ManageTrainingProgramSystem.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
		private ApplicationDbContext _context;
		private UserManager<ApplicationUser> _userManager;

		public AdminController()
		{
			_context = new ApplicationDbContext();
			_userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
		}
		// GET: Admin
		public ActionResult Index()
		{
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public ActionResult ShowTrainingStaffs()
		{
			var users = _context.Users.ToList();

			var TrainingStaff = new List<ApplicationUser>();

			foreach (var user in users)
			{
				if (_userManager.GetRoles(user.Id)[0].Equals("Training Staff"))
				{
					TrainingStaff.Add(user);
				}
			}

			return View(TrainingStaff);
		}

		[HttpGet]
		public ActionResult ShowTrainers()
		{
			var users = _context.Users.ToList();

			var Trainer = new List<ApplicationUser>();

			foreach (var user in users)
			{
				if (_userManager.GetRoles(user.Id)[0].Equals("Trainer"))
				{
					Trainer.Add(user);
				}
			}

			return View(Trainer);
		}

		public ActionResult Details(string id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var userId = id;
			var trainerInfo = _context.TrainersInfo.SingleOrDefault(u => u.UserId.Equals(id));

			if (trainerInfo == null) return HttpNotFound();

			return View(trainerInfo);
		}

		[HttpGet]
		public ActionResult Delete(string id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var userId = id;
			var userInfo = _context.TrainersInfo.SingleOrDefault(u => u.UserId.Equals(id));
			var user = _context.Users.SingleOrDefault(u => u.Id.Equals(id));

			if (user == null) return HttpNotFound();
			_context.TrainersInfo.Remove(userInfo);
			_context.Users.Remove(user);
			_context.SaveChanges();

			return RedirectToAction("ShowTrainers");
		}

		[HttpGet]
		public ActionResult Edit(string id)
		{
			var userId = id;
			var trainerInfo = _context.TrainersInfo.SingleOrDefault(u => u.UserId.Equals(id));

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

			return RedirectToAction("ShowTrainers");
		}

		[HttpGet]
		public ActionResult DeleteTrainingStaff(string id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var userId = id;
			var user = _context.Users.SingleOrDefault(u => u.Id.Equals(id));

			if (user == null) return HttpNotFound();
			_context.Users.Remove(user);
			_context.SaveChanges();

			return RedirectToAction("ShowTrainingStaffs");
		}

		[HttpGet]
		public ActionResult EditTrainingStaff(string id)
		{
			var userId = id;
			var UserInDb = _context.Users.SingleOrDefault(u => u.Id.Equals(id));

			if (UserInDb == null) return HttpNotFound();

			return View(UserInDb);
		}

		[HttpPost]
		public ActionResult EditTrainingStaff(ApplicationUser user)
		{
			var UserInDb = _context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));

			if (UserInDb == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			UserInDb.Email = user.Email;
			_context.SaveChanges();

			return RedirectToAction("ShowTrainingStaffs");
		}

		[HttpGet]
		public ActionResult ChangePassword(string id)
		{
			if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var userId = User.Identity.GetUserId();
			var UserInDb = _context.Users.SingleOrDefault(u => u.Id.Equals(id));

			if (UserInDb == null) return HttpNotFound();
			var viewModel = new ChangePasswordTrainerViewModel()
			{
				NewPassword = UserInDb.PasswordHash
			};
			return View(viewModel);
		}

		[HttpPost]
		public ActionResult ChangePassword(ApplicationUser user)
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new ChangePasswordTrainerViewModel()
				{
					NewPassword = user.PasswordHash
				};
				return View(user);
			}

			var userId = User.Identity.GetUserId();
			var UserInDb = _context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));

			if (UserInDb == null) return HttpNotFound();

			UserInDb.PasswordHash = user.PasswordHash;
			_context.SaveChanges();
			return RedirectToAction("ShowTrainers");

		}
	}
}

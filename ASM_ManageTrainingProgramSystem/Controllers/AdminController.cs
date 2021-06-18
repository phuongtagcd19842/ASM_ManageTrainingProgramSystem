using ASM_ManageTrainingProgramSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
	}
}

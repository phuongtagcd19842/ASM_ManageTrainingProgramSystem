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
    [Authorize(Roles = "Training Staff")]
    public class TraineesListController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TraineesListController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        // GET: TraineesList
        public ActionResult Index()
        {
            var users = _context.Users.ToList();

            var Trainees = new List<ApplicationUser>();

            foreach (var user in users)
            {
                if (_userManager.GetRoles(user.Id)[0].Equals("Trainee"))
                {
                    Trainees.Add(user);
                }
            }
            return View(Trainees);
        }

        public ActionResult Details(string id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var traineeInfo = _context.TraineesInfo.SingleOrDefault(t => t.UserId.Equals(id));
            if (traineeInfo == null) return HttpNotFound();
            return View(traineeInfo);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var traineeInfo = _context.TraineesInfo.SingleOrDefault(t => t.UserId.Equals(id));
            if (traineeInfo == null) return HttpNotFound();
            return View(traineeInfo);
        }
        [HttpPost]
        public ActionResult Edit(TraineeInfo traineeInfo)
        {
            if (traineeInfo == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            var traineeInfoInDb = _context.TraineesInfo.SingleOrDefault(t => t.UserId.Equals(traineeInfo.UserId));

            traineeInfoInDb.TraineeName = traineeInfo.TraineeName;
            traineeInfoInDb.Age = traineeInfo.Age;
            traineeInfoInDb.DateOfBirth = traineeInfo.DateOfBirth;
            traineeInfoInDb.Education = traineeInfo.Education;
            traineeInfoInDb.ProgrammingLanguage = traineeInfo.ProgrammingLanguage;
            traineeInfoInDb.TOEICScore = traineeInfo.TOEICScore;
            traineeInfoInDb.ExperienceDetail = traineeInfo.ExperienceDetail;
            traineeInfoInDb.Department = traineeInfo.Department;
            traineeInfoInDb.Location = traineeInfo.Location;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var user = _context.Users.SingleOrDefault(u => u.Id.Equals(id));
            var traineeInfo = _context.TraineesInfo.SingleOrDefault(u => u.UserId.Equals(id));

            if (user == null) return HttpNotFound();

            _context.TraineesInfo.Remove(traineeInfo);
            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
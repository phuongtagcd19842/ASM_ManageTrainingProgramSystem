using ASM_ManageTrainingProgramSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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

        [HttpGet]
        public ActionResult ViewCourses(string id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var courses = _context.TraineeCourses
                .Include(c => c.Course)
                .Where(c => c.UserId.Equals(id))
                .Select(c => c.Course);
            return View(courses);
        }

        [HttpGet]
        public ActionResult AssignCourses(string id)
        {
            var coursesInDb = _context.Courses.ToList();

            var assignCourses = _context.TraineeCourses
                .Include(a => a.Course)
                .Where(a => a.UserId.Equals(id))
                .Select(a => a.Course)
                .ToList();
            var coursesToAdd = new List<Course>();

            foreach(var course in coursesInDb)
            {
                if(!assignCourses.Contains(course))
                {
                    coursesToAdd.Add(course);
                }    
            }    
            return View(coursesToAdd);
        }
    }
}
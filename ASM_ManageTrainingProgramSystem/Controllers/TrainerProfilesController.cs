using ASM_ManageTrainingProgramSystem.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ASM_ManageTrainingProgramSystem.ViewModels;

namespace ASM_ManageTrainingProgramSystem.Controllers
{
    [Authorize(Roles = "Training Staff")]
    public class TrainerProfilesController : Controller
    {
        private ApplicationDbContext _context;
        public TrainerProfilesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: TrainerProfiles
        public ActionResult Index()
        {
            var trainerInfo = _context.TrainersInfo.ToList();

            if (trainerInfo == null) return HttpNotFound();

            return View(trainerInfo);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var trainerInfo = _context.TrainersInfo.SingleOrDefault(t => t.UserId.Equals(id));
            if (trainerInfo == null) return HttpNotFound();

            return View(trainerInfo);
        }

        [HttpPost]
        public ActionResult Edit(TrainerInfo trainerInfo)
        {
            var trainerInfoInDb = _context.TrainersInfo.SingleOrDefault(t => t.UserId.Equals(trainerInfo.UserId));
            if (trainerInfo == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            trainerInfoInDb.TrainerName = trainerInfo.TrainerName;
            trainerInfoInDb.ExternalOrInternal = trainerInfo.ExternalOrInternal;
            trainerInfoInDb.WorkingPlace = trainerInfo.WorkingPlace;
            trainerInfoInDb.Telephone = trainerInfo.Telephone;
            trainerInfoInDb.EmailAddress = trainerInfo.EmailAddress;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ViewCourses(string id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var courses = _context.TrainerCourses
                .Include(c => c.Course)
                .Where(c => c.UserId.Equals(id))
                .Select(c => c.Course);

            return View(courses);
        }
        [HttpGet]
        public ActionResult AssignCourse(string id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            if (_context.TrainersInfo.SingleOrDefault(t => t.UserId.Equals(id)).Equals(null)) return HttpNotFound();
            var coursesInDb = _context.Courses.ToList();

            var assignCourses = _context.TrainerCourses
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

            var viewModel = new TrainerCoursesViewModel
            {
                UserId = id,
                Courses = coursesToAdd
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult AssignCourse(TrainerCourse model)
        {
            var trainerCourse = new TrainerCourse
            {
                UserId = model.UserId,
                CourseId = model.CourseId
            };

            _context.TrainerCourses.Add(trainerCourse);
            _context.SaveChanges();
            return RedirectToAction("ViewCourses", new { id = model.UserId});
        }
    }
}
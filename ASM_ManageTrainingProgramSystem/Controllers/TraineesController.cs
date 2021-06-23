using ASM_ManageTrainingProgramSystem.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ASM_ManageTrainingProgramSystem.Controllers
{
    [Authorize(Roles ="Trainee")]
    public class TraineesController : Controller
    {
        private ApplicationDbContext _context;
        public TraineesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Trainees
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            var traineeInfo = _context.TraineesInfo.SingleOrDefault(t => t.UserId.Equals(userId));

            if (traineeInfo == null) return HttpNotFound();
            return View(traineeInfo);
        }

        public ActionResult ViewAllCourses()
        {
            var courses = _context.Courses
                .Include(c => c.Category)
                .ToList();
            return View(courses);
        }

        public ActionResult MyCourses()
        {
            var userId = User.Identity.GetUserId();
            var myCourses = _context.TraineeCourses
                .Include(m => m.Course)
                .Where(m => m.UserId.Equals(userId))
                .Select(m => m.Course)
                .ToList();
            if (myCourses == null) return HttpNotFound();
            return View(myCourses);
        }
    }
}
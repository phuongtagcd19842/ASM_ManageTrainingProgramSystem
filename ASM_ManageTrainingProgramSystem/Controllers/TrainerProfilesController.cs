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
    [Authorize(Roles ="Training Staff")]
    public class TrainerProfilesController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TrainerProfilesController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
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
    }
}
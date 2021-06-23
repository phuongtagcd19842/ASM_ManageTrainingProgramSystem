using ASM_ManageTrainingProgramSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ASM_ManageTrainingProgramSystem.ViewModels;
using Microsoft.Ajax.Utilities;

namespace ASM_ManageTrainingProgramSystem.Controllers
{
    [Authorize(Roles ="Training Staff")]
    public class CoursesController : Controller
    {
        private ApplicationDbContext _context;
        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Courses
        public ActionResult Index(string searchString)
        {
            var courses = _context.Courses
                .Include(c => c.Category)
                .ToList();

            if (!searchString.IsNullOrWhiteSpace())
            {
                courses = courses.Where(c => c.CourseName.Contains(searchString)).ToList();
            }

            return View(courses);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CourseCategoriesViewModel()
            {
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Course course)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CourseCategoriesViewModel()
                {
                    Course = course,
                    Categories = _context.Categories.ToList()
                };
                return View(viewModel);
            }

            var newCourse = new Course()
            {
                CourseName = course.CourseName,
                CategoryId = course.CategoryId,
                Description = course.Description,
            };

            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var courseInDb = _context.Courses
                .SingleOrDefault(c => c.Id == id);

            if (courseInDb == null) return HttpNotFound();

            var viewModel = new CourseCategoriesViewModel()
            {
                Course = courseInDb,
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Course course)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CourseCategoriesViewModel()
                {
                    Course = course,
                    Categories = _context.Categories.ToList()
                };
                return View(viewModel);
            }

            var courseInDb = _context.Courses
                .SingleOrDefault(c => c.Id == course.Id);

            if (courseInDb == null) return HttpNotFound();

            courseInDb.CourseName = course.CourseName;
            courseInDb.CategoryId = course.CategoryId;
            courseInDb.Description = course.Description;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var course = _context.Courses
                .SingleOrDefault(c => c.Id == id);

            if (course == null) return HttpNotFound();

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

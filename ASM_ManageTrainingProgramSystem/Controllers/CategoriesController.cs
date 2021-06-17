using ASM_ManageTrainingProgramSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASM_ManageTrainingProgramSystem.Controllers
{
    public class CategoriesController : Controller
    {
        private ApplicationDbContext _context;
        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Categories
        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            var newCategory = new Category()
            {
                CategoryName = category.CategoryName,
                Description = category.Description
            };

            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (categoryInDb == null) return HttpNotFound();
            Category category = categoryInDb;
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (!ModelState.IsValid) return View(category);
            
            var categoryInDb = _context.Categories.SingleOrDefault(c => c.Id == category.Id);

            if (categoryInDb == null) return HttpNotFound();

            categoryInDb.CategoryName = category.CategoryName;
            categoryInDb.Description = category.Description;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
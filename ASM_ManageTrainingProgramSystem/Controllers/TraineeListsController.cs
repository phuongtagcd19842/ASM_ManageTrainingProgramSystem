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
    public class TraineeListsController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        public TraineeListsController()
        {
            _context = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }
        // GET: TraineeLists
        public ActionResult Index()
        {
            var users = _context.Users.ToList();

            var Trainees = new List<ApplicationUser>();

            foreach(var user in users)
            {
                if(_userManager.GetRoles(user.Id)[0].Equals("Trainee"))
                {
                    Trainees.Add(user);
                }    
            }    
            return View(Trainees);
        }
    }
}
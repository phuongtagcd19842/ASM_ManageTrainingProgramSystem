using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ASM_ManageTrainingProgramSystem
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                    name: "RemoveCourse",
                    url: "{controller}/{action}/{id}/{courseId}",
                    new { controller = "TraineesList", action = "RemoveCourse", id = "", courseId = "" }
            );
            routes.MapRoute(
                    name: "RemoveCourseForTrainer",
                    url: "{controller}/{action}/{id}/{courseId}",
                    new { controller = "TrainerProfiles", action = "RemoveCourse", id = "", courseId = "" }
            );
        }
    }
}

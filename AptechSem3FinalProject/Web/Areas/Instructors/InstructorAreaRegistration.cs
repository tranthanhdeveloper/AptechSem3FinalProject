using System.Web.Mvc;

namespace Web.Areas.Instructor
{
    public class InstructorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Instructors";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Instructor_default",
                "Instructors/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Web.Areas.Instructors.Controllers" }
            );
        }
    }
}
using Service.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Web.Areas.Instructor.Models;
using Web.Areas.Instructors.Models;

namespace Web.Areas.Instructors.Controllers
{
    [Helper.Sercurity.Authorize]
    public class DashboardController : Controller
    {
        private ICourseService courseService;
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        public DashboardController(ICourseService courseService, IOrderService orderService, IUserService userService)
        {
            this.courseService = courseService;
            this.orderService = orderService;
            this.userService = userService;
        }

        // GET: Instructor/Dashboard
        [Helper.Sercurity.Authorize]
        public ActionResult Index()
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            var createdCourses = courseService.GetByCreatedUser(loggedUser).Take(10);
            var authorDashboardViewModel = new DashboardViewModels
            {
                AuthorSummaryInfoViewModel = new AuthorSummaryInfoViewModel(),
                AuthorCoursesViewModels = Mapper.Map<List<CourseItemViewModel>>(createdCourses)
            };

            foreach (var course in createdCourses)
            {
                authorDashboardViewModel.AuthorSummaryInfoViewModel.TotalUserEnrolled += course.Orders.Count();
            }
            authorDashboardViewModel.AuthorSummaryInfoViewModel.TotalCourses = createdCourses.Count();
            return View(authorDashboardViewModel);
        }

        //public ActionResult UserEnrollment()
        //{
        //    UserEnrollmentViewModel userEnrollmentViewModel = new UserEnrollmentViewModel();
        //    return View(userEnrollmentViewModel);
        //}
        
    }
}
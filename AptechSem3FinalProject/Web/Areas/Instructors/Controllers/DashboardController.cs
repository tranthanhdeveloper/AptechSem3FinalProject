using Service.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Web.Areas.Instructor.Models;
using Web.Areas.Instructors.Models;
using Model.Enum;
using Context.Database;

namespace Web.Areas.Instructors.Controllers
{
    [Helper.Sercurity.Authorize(RoleEnum.Author)]
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
        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Index()
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            var createdCourses = courseService.GetByCreatedUser(loggedUser).Take(10);
            var authorDashboardViewModel = new DashboardViewModels
            {
                AuthorSummaryInfoViewModel = CalculateSummaryInfo(createdCourses.ToList()),
                AuthorCoursesViewModels = Mapper.Map<List<CourseItemViewModel>>(createdCourses)
            };           
            return View(authorDashboardViewModel);
        }

        private AuthorSummaryInfoViewModel CalculateSummaryInfo(List<Course> courses) {
            var returnedValue = new AuthorSummaryInfoViewModel();
            returnedValue.TotalPricePaid = 0;
            foreach (var course in courses)
            {
                returnedValue.TotalUserEnrolled += course.Orders.Count();
                if(course.Orders.Count > 0)
                {
                    returnedValue.TotalPricePaid += course.Price?? 0;
                }
            }
            returnedValue.TotalCourses = courses.Count();

            return returnedValue;
        }
    }
}
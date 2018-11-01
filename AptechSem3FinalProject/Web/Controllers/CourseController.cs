using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Context.Database;
using Service.Service;
using Web.Models;


namespace Web.Controllers
{
    public class CourseController : Controller
    {
        #region Declare field and Constructors

        private ICourseService _courseService;
        private IUserService _userService;
        private ILectureService _lectureService;

        public CourseController(ICourseService courseService, IUserService userService, ILectureService lectureService)
        {
            _courseService = courseService;
            _userService = userService;
            _lectureService = lectureService;
        }

        #endregion

        #region Action methods

        // GET: Course
        public ActionResult Index()
        {
            var showCoursesViewModel = new ShowCoursesViewModel();
            var popularCourses = _courseService.GetAll();
            var lastedCourses = _courseService.GetLastedCourse();
            showCoursesViewModel.PopularCourses = Mapper.Map<List<CourseListItemViewModel>>(popularCourses);
            showCoursesViewModel.LastedCourses = Mapper.Map<List<CourseListItemViewModel>>(lastedCourses);
            return View(showCoursesViewModel);
        }

        public ActionResult CourseDetail(int id)
        {
            var courseDetailViewModel = new CourseDetailViewModel();
            var course = _courseService.GetById(id);
            courseDetailViewModel.CourseListItemViewModel = Mapper.Map<CourseListItemViewModel>(course);
            courseDetailViewModel.Author = _userService.GetById(course.UserId);
            courseDetailViewModel.CourseOutline = Mapper.Map< List<CourseOutlineViewModel>>(_lectureService.GetByCourseId(course.Id));
            return View(courseDetailViewModel);
        }

        public ActionResult CoursePlay(int id)
        {
            return View();
        }

        #endregion

        #region manipulate methods

        

        #endregion


    }
}
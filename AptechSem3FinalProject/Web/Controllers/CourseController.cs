using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using AutoMapper;
using Context.Database;
using Model.Enum;
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
        private IVideoService _videoService;
        private IOrderService _orderService;
        private ICommentService _commentService;
        private ICategoryService _categoryService;

        public CourseController(ICourseService courseService, IUserService userService, ILectureService lectureService, IVideoService videoService, IOrderService orderService, ICommentService commentService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _userService = userService;
            _lectureService = lectureService;
            _videoService = videoService;
            _orderService = orderService;
            _commentService = commentService;
            _categoryService = categoryService;
        }

        #endregion

        #region Action methods

        // GET: Course
        public ActionResult Index()
        {
            var showCoursesViewModel = new ShowCoursesViewModel();
            var popularCourses = _courseService.GetPublished().Take(8);
            var lastedCourses = _courseService.GetLastedCourse();
            showCoursesViewModel.PopularCourses = Mapper.Map<List<CourseItemViewModel>>(popularCourses);
            showCoursesViewModel.LastedCourses = Mapper.Map<List<CourseItemViewModel>>(lastedCourses);

            ViewBag.Category = _categoryService.GetAll();
            return View(showCoursesViewModel);
        }

        public ActionResult CourseDetail(int id)
        {
            var courseDetailViewModel = new CourseDetailViewModel();
            var course = _courseService.GetById(id);
            courseDetailViewModel.CourseListItemViewModel = Mapper.Map<CourseItemViewModel>(course);
            courseDetailViewModel.Author = _userService.GetById(course.UserId);
            courseDetailViewModel.CourseOutline = Mapper.Map<List<CourseOutlineViewModel>>(_lectureService.GetByCourseId(course.Id));
            courseDetailViewModel.RelatedCourses = Mapper.Map<List<CourseItemViewModel>>(course.Category.Courses
                    .OrderByDescending(relCourse => relCourse.Id).Take(5));
            if (Helper.Sercurity.SessionPersister.AccountInformation != null)
            {
                var loggedUserId = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
                courseDetailViewModel.IsPaid = _courseService.ValidateCourseAccessible(loggedUserId, id);
            }
            return View(courseDetailViewModel);
        }

        [Helper.Sercurity.Authorize]
        public ActionResult CoursePlay(int id, int? video)
        {
            var loggedUserId = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            if (_courseService.ValidateCourseAccessible(loggedUserId, id))
            {
                var course = _courseService.GetById(id);
                if(course.Status == (byte)CourseStatus.DELETED)
                {
                    return RedirectToAction("Errors", "NotFound");
                }
                if (!_orderService.GetOrderByCourseAndUser(course.Id, loggedUserId).Any() && course.UserId != loggedUserId)
                {
                    _orderService.AddOrder(course, loggedUserId, (int)(PaymentType.FREE));
                }

                var coursePlayViewModel = new CoursePlayViewModel
                {
                    CourseModuleViewModels = new List<CourseModuleViewModel>(),
                    CourseItemViewModel = Mapper.Map<CourseItemViewModel>(course)
                };
                foreach (var lecture in course.Lectures)
                {
                    var lectureMap = new CourseModuleViewModel
                    {
                        CourseLessonViewModels = Mapper.Map<List<CourseLessonViewModel>>(lecture.Videos),
                        Id = lecture.Id,
                        Name = lecture.Name
                    };
                    coursePlayViewModel.CourseModuleViewModels.Add(lectureMap);
                }

                coursePlayViewModel.CurrentLesson = video != null ? Mapper.Map<CourseLessonViewModel>(_videoService.GetById(video)) : coursePlayViewModel.CourseModuleViewModels.First().CourseLessonViewModels.First();
                return View(coursePlayViewModel);
            } else
            {
                return RedirectToAction("PaymentWithPaypal", "Payment", new { id = id });
            }

        }
        #endregion

        #region manipulate methods



        #endregion

        public string LoadMore(int offset)
        {
            var showCoursesViewModel = new LoadMoreCourseViewModel();
            var courses = _courseService.GetPublished().Skip(offset).Take(10);
            showCoursesViewModel.CourseItemViewModels = Mapper.Map<List<CourseItemViewModel>>(courses);
            return Helper.RenderHelper.RenderViewToString(ControllerContext, "LoadMore", showCoursesViewModel);
        }

        [HttpPost]
        public ActionResult Search(FormCollection formData)
        {
            IEnumerable<Course> createdCourses = new List<Course>();
            int cateId = 0;
            Int32.TryParse(Convert.ToString(formData["category"]), out cateId);
            string courseTitle = Convert.ToString(formData["title"]);
            if (cateId != 0)
            {
                createdCourses = _courseService.GetAll(course => course.Title.Contains(courseTitle) && course.CategoryId == cateId);
            }
            else
            {
                createdCourses = _courseService.GetAll(course => course.Title.Contains(courseTitle));
            }
            var returnModel = Mapper.Map<List<CourseItemViewModel>>(createdCourses);
            return Content(Helper.RenderHelper.RenderViewToString(ControllerContext, "~/Views/Course/Search.cshtml", returnModel));
        }
    }
}
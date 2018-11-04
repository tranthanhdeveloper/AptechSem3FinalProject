using System;
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
        private IVideoService _videoService;

        public CourseController(ICourseService courseService, IUserService userService, ILectureService lectureService, IVideoService videoService)
        {
            _courseService = courseService;
            _userService = userService;
            _lectureService = lectureService;
            _videoService = videoService;
        }

        #endregion

        #region Action methods

        // GET: Course
        public ActionResult Index()
        {
            var showCoursesViewModel = new ShowCoursesViewModel();
            var popularCourses = _courseService.GetAll();
            var lastedCourses = _courseService.GetLastedCourse();
            showCoursesViewModel.PopularCourses = Mapper.Map<List<CourseItemViewModel>>(popularCourses);
            showCoursesViewModel.LastedCourses = Mapper.Map<List<CourseItemViewModel>>(lastedCourses);
            return View(showCoursesViewModel);
        }

        public ActionResult CourseDetail(int id)
        {
            var courseDetailViewModel = new CourseDetailViewModel();
            var course = _courseService.GetById(id);
            courseDetailViewModel.CourseListItemViewModel = Mapper.Map<CourseItemViewModel>(course);
            courseDetailViewModel.Author = _userService.GetById(course.UserId);
            courseDetailViewModel.CourseOutline = Mapper.Map< List<CourseOutlineViewModel>>(_lectureService.GetByCourseId(course.Id));
            courseDetailViewModel.RelatedCourses =Mapper.Map<List<CourseItemViewModel>>(course.Category.Courses
                    .OrderByDescending(relCourse => relCourse.Id).Take(5));
            return View(courseDetailViewModel);
        }

        [Helper.Sercurity.Authorize]
        public ActionResult CoursePlay(int id, Nullable<int> video)
        {
            var coursePlayViewModel = new CoursePlayViewModel();
            coursePlayViewModel.CourseModuleViewModels = new List<CourseModuleViewModel>();
            var course = _courseService.GetById(id);
            coursePlayViewModel.CourseItemViewModel = Mapper.Map<CourseItemViewModel>(course);
            foreach (var lecture in course.Lectures)
            {
                var lectureMap = new CourseModuleViewModel();
                lectureMap.CourseLessonViewModels = Mapper.Map<List<CourseLessonViewModel>>(lecture.Videos);
                lectureMap.Id = lecture.Id;
                lectureMap.Name = lecture.Name;
                coursePlayViewModel.CourseModuleViewModels.Add(lectureMap);
            }
            return View(coursePlayViewModel);
        }
        #endregion

        #region manipulate methods

        

        #endregion


    }
}
using AutoMapper;
using Context.Database;
using Model.Enum;
using Service.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Instructor.Models;
using Web.Areas.Instructors.Models;
using Web.Helper;

namespace Web.Areas.Instructors.Controllers
{
    [Helper.Sercurity.Authorize(RoleEnum.Author)]
    public class CourseController : Controller
    {
        public const string STORED_IMAGES_DIRECTORY = "~/img/courses";
        private ICourseService courseService;
        private readonly IOrderService orderService;
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly IVideoService videoService;
        private readonly ILectureService lectureService;

        public CourseController(ICourseService courseService, IOrderService orderService, IUserService userService, ICategoryService categoryService, IVideoService videoService, ILectureService lectureService)
        {
            this.courseService = courseService;
            this.orderService = orderService;
            this.userService = userService;
            this.categoryService = categoryService;
            this.videoService = videoService;
            this.lectureService = lectureService;
        }

        // GET: Instructors/Course
        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Index()
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            var createdCourses = courseService.GetByCreatedUser(loggedUser);
            var authorDashboardViewModel = new DashboardViewModels
            {
                AuthorCoursesViewModels = Mapper.Map<List<CourseItemViewModel>>(createdCourses)
            };

            ViewBag.Category = categoryService.GetAll();
            return View(authorDashboardViewModel);
        }

        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Details(int id)
        {
            var courseItemView = new CourseItemViewModel { ModuleItemViewModels = new List<ModuleItemViewModel>() };
            var loggedUserId = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            var course = courseService.GetById(id);
            if (!courseService.ValidateCourseEditable(loggedUserId, id) || course.Status == (byte)CourseStatus.DELETED)
            {
                ViewData["EditCourseError"] = MessageConstants.EditCourseDeny;
                return View(courseItemView);
            }           
            courseItemView = Mapper.Map<CourseItemViewModel>(course);
            courseItemView.ModuleItemViewModels = new List<ModuleItemViewModel>();
            foreach (var module in course.Lectures)
            {
                var lesson = Mapper.Map<List<LessonItemViewModel>>(module.Videos);
                var moduleMapped = Mapper.Map<ModuleItemViewModel>(module);
                moduleMapped.LessonItemViewModels = lesson;
                courseItemView.ModuleItemViewModels.Add(moduleMapped);
            }
            ViewBag.PublishErrorMessage = TempData["PublishCourseDenied"] != null ? TempData["PublishCourseDenied"].ToString():"";
            return View(courseItemView);
        }


        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Create()
        {
            ViewBag.Category = categoryService.GetAll();
            return View(new CreateCourseViewModel());
        }


        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        [HttpPost]
        public ActionResult Create(CreateCourseViewModel createCourseViewModel, HttpPostedFileBase image)
        {
            ViewBag.Category = categoryService.GetAll();
            if (image == null)
            {
                ModelState.AddModelError("", MessageConstants.ImageRequired);
            }
            if (ModelState.IsValid)
            {
                var course = new Course();
                course = Mapper.Map<Course>(createCourseViewModel);
                course.Image = image.FileName;
                course.CreateDate = DateTime.Now;
                course.UserId = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
                course.Status = 1;
                courseService.Add(course);

                //Save image to file                
                var filename = image.FileName;
                var filePathOriginal = Server.MapPath(STORED_IMAGES_DIRECTORY);
                string savedFileName = Path.Combine(filePathOriginal, filename);
                image.SaveAs(savedFileName);
                return RedirectToAction("Details", new { id = course.Id });
            }
            return View(createCourseViewModel);
        }

        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Edit(int id)
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            if (!courseService.ValidateCourseEditable(loggedUser, id))
            {
                ViewData["EditCourseError"] = MessageConstants.EditCourseDeny;
                return View();
            }
            ViewBag.Category = categoryService.GetAll();
            var editCourse = Mapper.Map<EditCourseViewModel>(courseService.GetById(id));
            return View(editCourse);
        }

        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        [HttpPost]
        public ActionResult Edit(EditCourseViewModel editCourseViewModel, HttpPostedFileBase image)
        {            
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            if (!courseService.ValidateCourseEditable(loggedUser, editCourseViewModel.Id))
            {
                ViewBag.Category = categoryService.GetAll();
                ViewData["EditCourseError"] = MessageConstants.EditCourseDeny;
                return View();
            }

            if (ModelState.IsValid)
            {
                var course = courseService.GetById(editCourseViewModel.Id);

                if (image != null)
                {

                    var filenameUpdate = Guid.NewGuid().ToString()+Path.GetExtension(image.FileName);
                    FileHelper.StoreFile(Server.MapPath(STORED_IMAGES_DIRECTORY), filenameUpdate, image);
                    try
                    {
                        if (System.IO.File.Exists(Path.Combine(Server.MapPath(STORED_IMAGES_DIRECTORY), course.Image)))
                        {
                            System.IO.File.Delete(Path.Combine(Server.MapPath(STORED_IMAGES_DIRECTORY), course.Image));
                        }
                    }catch(Exception e)
                    {

                    }
                    course.Image = filenameUpdate;
                }
                course.Title = editCourseViewModel.Title;
                course.Description = editCourseViewModel.Description;
                course.Comment = editCourseViewModel.Comment;
                course.Price = editCourseViewModel.Price;
                course.CategoryId = editCourseViewModel.CategoryId;
                course.Prerequisites = editCourseViewModel.Prerequisites;
                courseService.Update(course);

                return RedirectToAction("Details", "Course", new {course.Id});
            }
            ViewBag.Category = categoryService.GetAll();
            return View(editCourseViewModel);
        }

        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Publish(int id)
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            var courseToPublished = courseService.GetById(id);
            if (courseToPublished.UserId != loggedUser)
            {
                TempData["EditCourseError"] = MessageConstants.EditCourseDeny;
                return View();
            }
            var lessonCounter = 0;
            foreach(var module in courseToPublished.Lectures)
            {
                lessonCounter += module.Videos.Count;
            }
            if (courseToPublished.Lectures.Count <= 0 || lessonCounter <= 0)
            {
                TempData["PublishCourseDenied"] = MessageConstants.PublishCourseDenied;
                return RedirectToAction("Details", new { id });
            }
            courseToPublished.Status = (byte)CourseStatus.PUBLISHED;
            courseService.Update(courseToPublished);
            return RedirectToAction("Index");
        }

        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult UnPublish(int id)
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            var courseToPublished = courseService.GetById(id);
            if (courseToPublished.UserId != loggedUser)
            {
                TempData["EditCourseError"] = MessageConstants.EditCourseDeny;
                return View();
            }            
            courseToPublished.Status = (byte)CourseStatus.CREATED;
            courseService.Update(courseToPublished);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Search(CourseSearchViewModel searchOption)
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            IEnumerable<Course> createdCourses = new List<Course>();
            if(searchOption.CategoryId != 0)
            {
                createdCourses = courseService.GetAll(course => course.Title.Contains(searchOption.Title) && course.CategoryId == searchOption.CategoryId);
            }
            else
            {
                createdCourses = courseService.GetAll(course => course.Title.Contains(searchOption.Title));
            }
            var courseSearchResultView = Mapper.Map<List<CourseItemViewModel>>(createdCourses);
            return PartialView("Search", courseSearchResultView);
        }

        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Delete(int id)
        {
            try
            {
                var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
                if (!courseService.ValidateCourseEditable(loggedUser, id))
                {
                    TempData["EditCourseError"] = MessageConstants.EditCourseDeny;
                    return RedirectToAction("Details", new { id });
                }
                var courseToBeDelete = courseService.GetById(id);
                courseToBeDelete.Status = (byte)CourseStatus.DELETED;
                courseService.Update(courseToBeDelete);
                return RedirectToAction("Index", new { courseToBeDelete.Id });
            }
            catch
            {
                return RedirectToAction("Index", new { id });
            }
        }
    }
}
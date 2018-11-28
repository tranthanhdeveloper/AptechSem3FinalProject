using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AutoMapper;
using Context.Database;
using Model.Enum;
using PagedList;
using Service.Service;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Helper.Sercurity.Authorize(RoleEnum.Admin)]
    public class CategoryController : AdminController
    {
        private readonly FileMimeTypes fileMimeTypes = new FileMimeTypes();
        private ICategoryService _categoryService;
        private ICourseService _courseService;
        private ILectureService _lectureService;
        private IVideoService _videoService;

        public CategoryController(ICategoryService categoryService, ILectureService lectureService, ICourseService courseService, IVideoService videoService)
        {
            _categoryService = categoryService;
            _lectureService = lectureService;
            _courseService = courseService;
            _videoService = videoService;
        }
        // GET: Admin/Course
        public ActionResult ManageCategory(int? page)
        {
            var listCategory = _categoryService.GetAll().OrderByDescending(u => u.id) as IEnumerable<Category>;
            var pageSize = int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            return View(listCategory.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult FilterCategory(int? page, string Model_Name)
        {
            var pageSize = int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            var listCategory = _categoryService.GetAll().OrderByDescending(u => u.id) as IEnumerable<Category>;
            if (Model_Name != "")
            {
                listCategory = listCategory.Where(l => l.CategoryName.ToLower().Contains(Model_Name.ToLower()));
            }

            return PartialView("~/Areas/Admin/Views/Course/_partial_Search_Category.cshtml", listCategory.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ManageTreeList(int? page)
        {
            var listCategory = _categoryService.GetAll().OrderByDescending(u => u.id) as IEnumerable<Category>;
            var pageSize = int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            return View(listCategory);
        }

        public ActionResult GetCourse(int id)
        {
            var category = _categoryService.GetById(id);
            return PartialView("~/Areas/Admin/Views/Category/_partial_Search_CourseTreeList.cshtml", category.GetCourses());
        }

       

        public ActionResult EditCategory(Category model)
        {
            var category = _categoryService.GetById(model.id);
            if (category != null)
            {
                category.CategoryName = model.CategoryName;
                category.Description = model.Description;


                _categoryService.Update(category);
            }

            return Success();
        }

        public ActionResult GetEditCategory(int id)
        {
            var category = _categoryService.GetById(id);
            return PartialView("~/Areas/Admin/Views/Category/_partial_Edit_Category.cshtml", category);
        }
        public ActionResult GetEditCourse(int id)
        {
            var course = _courseService.GetById(id);
            return PartialView("~/Areas/Admin/Views/Category/_partial_Edit_Course.cshtml", course);
        }

        #region Lecture

        public ActionResult GetLecture(int id)
        {
            var course = _courseService.GetById(id);
            return PartialView("~/Areas/Admin/Views/Category/_partial_Search_LectureTreeList.cshtml", course.GetLectures());
        }

        
        public ActionResult GetEditLecture(int id)
        {
            var lecture = _lectureService.GetAll(l => l.Id == id).FirstOrDefault();
            return PartialView("~/Areas/Admin/Views/Category/_partial_Edit_Lecture.cshtml", lecture);
        
        }

        public ActionResult EditLecture(Lecture model)
        {
            var lecture = _lectureService.GetAll(l => l.Id == model.Id).FirstOrDefault();//_lectureService.GetById(model.Id);
            if (lecture != null)
            {
                lecture.Name = model.Name;
                _lectureService.Update(lecture);
            }

            return Success();
        }

        public ActionResult DeleteLecture(int id)
        {
            var lecture = _lectureService.GetAll(l => l.Id == id).FirstOrDefault();
            var listvideo = lecture.Videos;
            foreach(var lesson in listvideo.ToArray())
            {
                _videoService.Delete(lesson);
            }
            _lectureService.Delete(lecture);
            return Success();
        }

        #endregion

        #region Lesson

        public ActionResult GetLesson(int id)
        {
            var lecture = _lectureService.GetAll(l => l.Id == id).FirstOrDefault();
            return PartialView("~/Areas/Admin/Views/Category/_partial_Search_LessonTreeList.cshtml", lecture.GetVideos());
        }

        public ActionResult GetEditLesson(int id)
        {
            var lesson = _videoService.GetById(id);
            var editLessonViewModel = new EditLessonViewModel
            {
                Id = lesson.Id,
                IsEnable =  lesson.IsEnable == 1,
                IsPreview = lesson.IsPreview ==1,
                Title = lesson.Title
            };
            return PartialView("~/Areas/Admin/Views/Category/_partial_Edit_Lesson.cshtml", editLessonViewModel);
        }

        public ActionResult DeleteLesson(int id)
        {
            var lesson = _videoService.GetById(id);
            _videoService.Delete(lesson);
            return Success();
        }

        public ActionResult EditLesson(EditLessonViewModel formData)
        {
            //var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            var oldLesson = _videoService.GetById(formData.Id);
            //if(oldLesson.Lecture.Course.UserId != loggedUser)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            //}
            oldLesson.Title = formData.Title;
            oldLesson.IsEnable = formData.IsEnable ? (byte)1 : (byte)0;
            oldLesson.IsPreview = formData.IsPreview ? (byte)1 : (byte)0;

            // begin checking if user has put a video to update lesson
            if (formData.uploadVideo != null && fileMimeTypes.AcceptedFileType.ContainsValue(formData.uploadVideo.ContentType))
            {
                var uidFileName = Guid.NewGuid().ToString() + Path.GetExtension(formData.uploadVideo.FileName);
                string fileSavePath = "";
                try
                {
                    fileSavePath = Path.Combine(WebConfigurationManager.AppSettings["DefaultVideoDirectory"], uidFileName);
                    formData.uploadVideo.SaveAs(fileSavePath);
                }
                catch
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                
                // delete video
                if (System.IO.File.Exists(Path.Combine(WebConfigurationManager.AppSettings["DefaultVideoDirectory"], oldLesson.Path)))
                {
                    System.IO.File.Delete(fileSavePath);
                    ViewBag.deleteSuccess = "true";
                }
                oldLesson.Path = uidFileName;
            }
            _videoService.Update(oldLesson);
           
            return Success();
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Context.Database;
using Model.Enum;
using PagedList;
using Service.Service;

namespace Web.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        private ICategoryService _categoryService;
        private ICourseService _courseService;
        private ILectureService _lectureService;

        public CategoryController(ICategoryService categoryService, ILectureService lectureService, ICourseService courseService)
        {
            _categoryService = categoryService;
            _lectureService = lectureService;
            _courseService = courseService;
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

        public ActionResult GetLecture(int id)
        {
            var course = _courseService.GetById(id);
            return PartialView("~/Areas/Admin/Views/Category/_partial_Search_LectureTreeList.cshtml", course.GetLectures());
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
    }
}
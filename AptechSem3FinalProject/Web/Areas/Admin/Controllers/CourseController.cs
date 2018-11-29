﻿using System;
using System.Collections.Generic;
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
    [Helper.Sercurity.Authorize(RoleEnum.Admin)]
    public class CourseController : AdminController
    {
        private ICourseService _courseService;
        private IUserService _userService;

        public CourseController(ICourseService courseService, IUserService userService)
        {
            _courseService = courseService;
            _userService = userService;
        }
        // GET: Admin/Course
        public ActionResult ManageCourse(int? page, int userId)
        {
            var listCourse = _courseService.GetAll(u => u.UserId == userId).OrderByDescending(u => u.Id) as IEnumerable<Course>;
            var pageSize =int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            ViewBag.userId = userId;
            ViewBag.Name_User = _userService.GetById(userId).Name;
            return View(listCourse.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult FilterCourse(int? page, string Model_Name, int userId)
        {
            var pageSize = int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            var listCourse = _courseService.GetAll(u => u.UserId == userId).OrderByDescending(u => u.Id) as IEnumerable<Course>;
            if (Model_Name != "")
            {
                listCourse = listCourse.Where(l => l.Title.ToLower().Contains(Model_Name.ToLower()));
            }

            return PartialView("~/Areas/Admin/Views/Course/_partial_Search_Course.cshtml", listCourse.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult EditCourse(Course model)
        {
            var course = _courseService.GetById(model.Id);
            if (course != null)
            {
                course.Title = model.Title;
                course.Description = model.Description;
                course.Comment = model.Comment;
                course.Price = model.Price;
                course.Prerequisites = model.Prerequisites;


                _courseService.Update(course);
            }

            return Success();
        }

        public ActionResult DeleteCourse(int id)
        {
            var course = _courseService.GetById(id);
            course.Status = (int) CourseStatus.DELETED;
            _courseService.Update(course);
            return Success();
        }
    }
}
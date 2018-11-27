using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Context.Database;
using Context.Repository;
using Service.Service;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _serviceUser;
        private readonly ICourseService _courseService;

        public HomeController(IUserService serviceUser, ICourseService courseService)
        {
            _serviceUser = serviceUser;
            _courseService = courseService;
        }

        public ActionResult Index()
        {
            var homeModel = new HomeViewModel();
            homeModel.InteractiveCourses = Mapper.Map<List<CourseItemViewModel>>(_courseService.GetInteractiveCourses().ToList());
            homeModel.LastedCourses = Mapper.Map<List<CourseItemViewModel>>(_courseService.GetLastedCourse().ToList());
            return View(homeModel);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CourseDetail(int id)
        {
            return View();
        }

        public ActionResult CoursePlay(int id)
        {
            return View();
        }
    }
}
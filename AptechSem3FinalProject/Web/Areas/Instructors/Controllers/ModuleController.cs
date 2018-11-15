using AutoMapper;
using Context.Database;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Instructors.Models;

namespace Web.Areas.Instructors.Controllers
{
    public class ModuleController : Controller
    {
        private readonly IUserService userService;
        private readonly ILectureService lectureService;
        private readonly ICourseService courseService;

        public ModuleController(IUserService userService, ILectureService lectureService, ICourseService courseService)
        {
            this.userService = userService;
            this.lectureService = lectureService;
            this.courseService = courseService;
        }
        // GET: Instructors/Module
        public ActionResult Index()
        {
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        // GET: Instructors/Module/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Instructors/Module/Create
        [Helper.Sercurity.Authorize]
        [HttpPost]
        public ActionResult Create(CreateModuleViewModule formData)
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            if (!ModelState.IsValid)
            {
                new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            }

            var course = courseService.GetById(formData.CourseId);
            if (course == null || course.UserId != loggedUser)
            {
                new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            var moduleToBeSaved = new Lecture
            {
                Name = formData.Title,
                Status = (byte)1,
                CourseId = formData.CourseId
            };
            var savedModule = lectureService.Add(moduleToBeSaved);
            return Content(Helper.RenderHelper.RenderViewToString( ControllerContext, "Create", Mapper.Map<ModuleViewModel>(savedModule)));
        }

        // GET: Instructors/Module/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Instructors/Module/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Instructors/Module/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Instructors/Module/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

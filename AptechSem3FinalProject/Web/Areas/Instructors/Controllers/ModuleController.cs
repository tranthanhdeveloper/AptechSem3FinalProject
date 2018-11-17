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
        public ActionResult Create(CreateModuleViewModel formData)
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
                Name = formData.Name,
                Status = (byte)1,
                CourseId = formData.CourseId
            };
            var savedModule = lectureService.Add(moduleToBeSaved);
            return Content(Helper.RenderHelper.RenderViewToString( ControllerContext, "Create", Mapper.Map<ModuleItemViewModel>(savedModule)));
        }

        // POST: Instructors/Module/Edit/5
        [HttpPost]
        public ActionResult Edit(EditModuleViewModel formData)
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            if (!lectureService.IsEditable(loggedUser, formData.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (!ModelState.IsValid)
            {
                new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            }
            var moduleToBeUpdated = lectureService.Get(formData.Id);
            moduleToBeUpdated.Name = formData.Name;
            lectureService.Update(moduleToBeUpdated);
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject( Mapper.Map<ModuleItemViewModel>(lectureService.Get(formData.Id))));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
                if (!lectureService.IsEditable(loggedUser, id))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                }
                lectureService.Delete(id);
                return new HttpStatusCodeResult(HttpStatusCode.OK);

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
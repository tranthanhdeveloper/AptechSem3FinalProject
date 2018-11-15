using AutoMapper;
using Context.Database;
using Service.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Web.Areas.Instructors.Models;

namespace Web.Areas.Instructors.Controllers
{
    [Helper.Sercurity.Authorize]
    public class LessonController : Controller
    {
        private readonly FileMimeTypes fileMimeTypes = new FileMimeTypes();
        private readonly IUserService userService;
        private readonly IVideoService videoService;
        private readonly ILectureService lectureService;

        public LessonController(IUserService userService, IVideoService videoService, ILectureService lectureService)
        {
            this.userService = userService;
            this.videoService = videoService;
            this.lectureService = lectureService;
        }
        // GET: Instructors/Lesson
        public ActionResult Index()
        {
            return View();
        }

        // GET: Instructors/Lesson/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.CourseId = videoService.GetById(id).Lecture.CourseId;
            return View();
        }

        // POST: Instructors/Lesson/Create
        [HttpPost]
        [Helper.Sercurity.Authorize]
        public ActionResult Create(CreateLessonViewModel formData, HttpPostedFileBase uploadVideo)
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            var module = lectureService.GetAll(mod => mod.Id == formData.ModuleId).First();
            if (module.Course.UserId != loggedUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            }
            else
            {
                if (module != null && uploadVideo != null && fileMimeTypes.AcceptedFileType.ContainsValue(uploadVideo.ContentType))
                {
                    // storing file to hard disk
                    var uidFileName = Guid.NewGuid().ToString()+Path.GetExtension(uploadVideo.FileName);
                    try
                    {
                        var fileSavePath = Path.Combine(WebConfigurationManager.AppSettings["DefaultVideoDirectory"], uidFileName);
                        uploadVideo.SaveAs(fileSavePath);
                    }
                    catch
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                    }
                    var lessonToBeSaved = new Video
                    {
                        CreatedDate = DateTime.Now,
                        CourseId = module.CourseId,
                        IsEnable = (byte)1,
                        IsPreview = formData.IsPreview ? (byte)1 : (byte)0,
                        Path = uidFileName,
                        Title = formData.Title,
                        LectureId = module.Id
                    };
                    videoService.Insert(lessonToBeSaved);
                    ViewBag.CourseId = module.CourseId;
                    return Content(Helper.RenderHelper.RenderViewToString(ControllerContext, "Create", Mapper.Map<LessonViewModel>(lessonToBeSaved)));
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        // GET: Instructors/Lesson/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Instructors/Lesson/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EditLessonViewModel formData, HttpPostedFileBase uploadVideo)
        {

            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            var oldLesson = videoService.GetById(id);
            if(oldLesson.Lecture.Course.UserId != loggedUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            }
            oldLesson.Title = formData.Title;
            oldLesson.IsEnable = formData.IsEnable ? (byte)1 : (byte)0;
            oldLesson.IsPreview = formData.IsPreview ? (byte)1 : (byte)0;
            videoService.Update(oldLesson);
            return Json(Newtonsoft.Json.JsonConvert.SerializeObject(Mapper.Map<LessonViewModel>(videoService.GetById(id))));
        }    

        // GET: Instructors/Lesson/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Instructors/Lesson/Delete/5
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

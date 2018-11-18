using AutoMapper;
using Context.Database;
using Model.Enum;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Web.Areas.Instructors.Models;
using static System.Net.WebRequestMethods;

namespace Web.Areas.Instructors.Controllers
{
    [Helper.Sercurity.Authorize(RoleEnum.Author)]
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
        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Instructors/Lesson/Details/5
        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Details(int id)
        {
            ViewBag.CourseId = videoService.GetById(id).Lecture.CourseId;
            return View();
        }

        // POST: Instructors/Lesson/Create
        [HttpPost]
        [Helper.Sercurity.Authorize(RoleEnum.Author)]
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
                    return Content(Helper.RenderHelper.RenderViewToString(ControllerContext, "Create", Mapper.Map<LessonItemViewModel>(lessonToBeSaved)));
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        // GET: Instructors/Lesson/Edit/5
        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Edit(int id)
        {
            return Content(Helper.RenderHelper.RenderViewToString(ControllerContext, "Edit", Mapper.Map<LessonItemViewModel>(videoService.GetById(id))));
        }

        // POST: Instructors/Lesson/Edit/5
        [Helper.Sercurity.Authorize(RoleEnum.Author)]
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

            // begin checking if user has put a video to update lesson
            if (uploadVideo != null && fileMimeTypes.AcceptedFileType.ContainsValue(uploadVideo.ContentType))
            {
                var uidFileName = Guid.NewGuid().ToString() + Path.GetExtension(uploadVideo.FileName);
                string fileSavePath = "";
                try
                {
                    fileSavePath = Path.Combine(WebConfigurationManager.AppSettings["DefaultVideoDirectory"], uidFileName);
                    uploadVideo.SaveAs(fileSavePath);
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
            videoService.Update(oldLesson);
            var updatedLesson = Mapper.Map<LessonItemViewModel>(videoService.GetById(id));
            return Content(Helper.RenderHelper.RenderViewToString(ControllerContext, "Create", updatedLesson));
        }

        // GET: Instructors/Lesson/Delete/5
        [Helper.Sercurity.Authorize(RoleEnum.Author)]
        public ActionResult Delete(int id)
        {
            var loggedUser = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
            var lessonToDelete = videoService.GetById(id);
            if (lessonToDelete.Lecture.Course.UserId != loggedUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            }

            try
            {
                videoService.Delete(lessonToDelete);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            
        }
    }
}
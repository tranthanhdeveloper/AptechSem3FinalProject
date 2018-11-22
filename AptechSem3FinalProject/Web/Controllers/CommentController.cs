using Context.Database;
using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ICommentService commentService;
        private readonly IUserService userService;
        public CommentController(ICourseService courseService, ICommentService commentService, IUserService userService)
        {
            this.courseService = courseService;
            this.commentService = commentService;
            this.userService = userService;
        }

        // GET: Comment
        public ActionResult Index(int id)
        {            
            return Content(Helper.RenderHelper.RenderViewToString(ControllerContext,"Index", commentService.GetByCourse(id).ToList()));
        }
       
        // POST: Comment/Create
        [HttpPost]
        [Helper.Sercurity.Authorize]
        public ActionResult Create(int id, FormCollection collection)
        {
            if(courseService.GetById(id) == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Course not found");
            }
            try
            {
                var comment = new Comment();
                comment.Content = collection.Get("Content");
                comment.CreationDate = DateTime.Now;
                comment.UserId = Helper.Sercurity.SessionPersister.AccountInformation.UserId;
                comment.CourseId = id;
                var createdComment = commentService.Add(comment);
                createdComment.User = userService.GetById(createdComment.UserId);

                return PartialView("CommentItem", createdComment);
            }
            catch
            {
                return View();
            }
        }
        
    }
}

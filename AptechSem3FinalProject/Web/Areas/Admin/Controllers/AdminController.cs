using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public class JsonResultModel
        {
            //success = true, message = message, reload = reload, redirect = redirect
            public bool success { get; set; }
            public string message { get; set; }
            public bool reload { get; set; }
            public string redirect { get; set; }
        }

        public class SuccessJsonResultModel : JsonResultModel
        {
            public SuccessJsonResultModel() : base()
            {
                success = true;
            }
        }
        protected ActionResult Success(string message = "", bool reload = true, string redirect = "", object data = null)
        {
            return Json(new SuccessJsonResultModel
            {
                message = message,
                reload = reload,
                redirect = redirect
            });
        }
        protected override PartialViewResult PartialView(string view, object model)
        {
            Success();
            return base.PartialView(view, model);
        }
    }
}
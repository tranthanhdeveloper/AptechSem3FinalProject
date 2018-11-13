using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Enum;
using Web.Helper.Sercurity;

namespace Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        //[Helper.Sercurity.Authorize(RoleEnum.Admin)]
        public ActionResult Index()
        {
            return View();
        }
    }
}
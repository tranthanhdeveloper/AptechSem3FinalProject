using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Context.Database;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var a = new AptechSem3FinalProjectEntities();
            var c = a.Users.ToList();
            return View();
        }
    }
}
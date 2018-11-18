using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Context.Database;
using Context.Repository;
using Service.Service;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _serviceUser;

        public HomeController(IUserService serviceUser)
        {
            _serviceUser = serviceUser;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}
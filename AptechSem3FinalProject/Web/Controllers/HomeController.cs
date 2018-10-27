using System;
using System.Collections.Generic;
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
        private IUserService _serviceUser;

        public HomeController(IUserService serviceUser)
        {
            _serviceUser = serviceUser;
        }
        public ActionResult Index()
        {
           
            var a = _serviceUser.GetAll();
            var u = _serviceUser.GetById(1);
            u.Address = "456";
            _serviceUser.Update(u);
           // _serviceUser.Save();
            return View();


        }
    }
}
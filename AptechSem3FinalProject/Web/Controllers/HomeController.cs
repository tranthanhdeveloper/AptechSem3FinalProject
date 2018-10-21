using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Context.Database;
using Context.Repository;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var a = new AptechSem3FinalProjectEntities();
            var c = a.Users.ToList();
            var uow = new Uow<User>();
            var user = new User{Name = "Thanh", Email = "Thanh@gmail.com", Address = "Ha Tinh"};
            var aaa = uow.Repository.GetById(1);
            uow.Repository.Insert(user);
            uow.Save();
            return View();
        }
    }
}
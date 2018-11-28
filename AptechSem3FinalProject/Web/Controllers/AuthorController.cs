using Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IUserService userService;
        public AuthorController(IUserService userService)
        {
            this.userService = userService;
        }

        public ActionResult Details(int id)
        {
            return View(userService.GetById(id));
        }

        public ActionResult PaymentDetail(int id)
        {
            return View(userService.GetById(id));
        }

    }
}

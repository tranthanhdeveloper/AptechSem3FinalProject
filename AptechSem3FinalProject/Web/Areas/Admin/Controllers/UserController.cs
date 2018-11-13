using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Context.Database;
using PagedList;
using Service.Service;

namespace Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Admin/User
        public ActionResult ListUser(int? page)
        {
            var listUser = _userService.GetAll().OrderByDescending(u => u.Id) as IEnumerable<User>;
            var pageSize =int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            return View(listUser.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult FilterUser(int? page, string name)
        {
            var pageSize = int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            var listUser = _userService.GetAll();
            if (name != null)
            {
                listUser = listUser.Where(l => l.Name.ToLower().Contains(name.ToLower()));
            }

            return View("ListUser", listUser.ToPagedList(pageNumber, pageSize));
        }
    }
}
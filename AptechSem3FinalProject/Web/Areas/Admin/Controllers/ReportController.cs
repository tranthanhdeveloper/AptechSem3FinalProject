using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Model.Enum;
using PagedList;
using Service.Service;

namespace Web.Areas.Admin.Controllers
{
    [Helper.Sercurity.Authorize(RoleEnum.Admin)]
    public class ReportController : AdminController
    {
        private IUserService _userService;

        public ReportController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: Admin/Report
        public ActionResult ReportByAuthor(int? page)
        {
            var users = _userService.GetAll(u => u.Accounts.Any(a => a.RoleId == (int) RoleEnum.Author));
            var pageSize =int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            return View(users.ToPagedList(pageNumber, pageSize));
        }
    }
}
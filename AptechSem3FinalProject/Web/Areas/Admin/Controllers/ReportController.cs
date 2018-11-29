using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Model.Enum;
using PagedList;
using Service.Service;
using Web.Areas.Admin.Models;

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
            var report = _userService.GetAll(u => u.Accounts.Any(a => a.RoleId == (int) RoleEnum.Author)).Select(rp => new ReportModel
            {
                UserId = rp.Id,
                Name = rp.Name,
                CountCourse = rp.Courses.Count,
                CountOrder = rp.Courses.Select(c => c.Orders.Count).Sum(),
                TotalMoney = rp.Courses.Sum(c => c.Orders.Sum(o => int.Parse(o.Course.Price.ToString()) ))
            });

            var pageSize =int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            
            return View(report.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult FilterReportAuthor(int? page, string Model_Name)
        {
            var users = _userService.GetAll(u =>  u.Accounts.Any(a => a.RoleId == (int) RoleEnum.Author));
            IEnumerable<ReportModel> reports = null;
            if (Model_Name != "")
            {
                users = users.Where(u => u.Name.ToLower().Contains(Model_Name.ToLower()));
            }
            var report = users.Select(rp => new ReportModel
            {
                UserId = rp.Id,
                Name = rp.Name,
                CountCourse = rp.Courses.Count,
                CountOrder = rp.Courses.Select(c => c.Orders.Count).Sum(),
                TotalMoney = rp.Courses.Sum(c => c.Orders.Sum(o => int.Parse(o.Course.Price.ToString()) ))
            });

            var pageSize =int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            return PartialView("~/Areas/Admin/Views/Report/_partial_Search_ReportByAuthor.cshtml",report.ToPagedList(pageNumber, pageSize));
        }
    }
}
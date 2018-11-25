using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Service;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        private ICourseService _courseService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        public DashboardController(ICourseService courseService, IOrderService orderService, IUserService userService)
        {
            _courseService = courseService;
            _orderService = orderService;
            _userService = userService;
        }
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var totalPrice = _orderService.GetAll().Select(o => o.Course.Price);
            
            ViewBag.TotalCourse = _courseService.GetAll().Count();
            ViewBag.TotalOrder = _orderService.GetAll().Count();
            ViewBag.TotalUser = _orderService.GetAll().Count();
            ViewBag.TotalMoney = totalPrice.Sum();
            return View();
        }

        public ActionResult ChartReport()
        {
            object report = null;
            var today = DateTime.Now;
            var start = StartOfMonth(today);
            var end = EndOfMonth(today);
            var course =
                _courseService.GetAll(c => c.CreateDate.HasValue && c.CreateDate > start && c.CreateDate < end);
            var order = _orderService.GetAll(c => c.CreatedDate > start && c.CreatedDate < end);
            var reports = new List<ReportModel>();
            foreach (var date in EachDay(start, end))
            {
                var countCourse = course.Count(h => h.CreateDate.HasValue && ToDateString(h.CreateDate.Value).Contains(ToDateString(date)));
                var countOrder =
                    order.Count(s => ToDateString(s.CreatedDate).Contains(ToDateString(date)));
                

                reports.Add(new ReportModel
                {
                    CountCourse = countCourse,
                    CountOrder= countOrder,
                    CreatedDate = date
                });}

            if (reports != null)
            {
                Func<DateTime, string> pred = d => d.ToString("d/M/yyyy");

                report = reports.Select(r => new { Date=pred(r.CreatedDate), r.CountOrder, r.CountCourse });
            }

            return Json(report);
        }

        public static DateTime StartOfMonth( DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime EndOfMonth( DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        }

        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for(var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static string ToDateString( DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Context.Database;
using Model.Enum;
using PagedList;
using Service.Service;
using WebGrease.Css.Extensions;

namespace Web.Areas.Admin.Controllers
{
    [Helper.Sercurity.Authorize(RoleEnum.Admin)]
    public class OrderController : Controller
    {
        private IOrderService _orderService;
        private IUserService _userService;

        public OrderController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }
        // GET: Admin/Order
        public ActionResult ManageOrder(int? page, int userId)
        {
            
            var orders = _orderService.GetAll(o => o.Course.UserId == userId).OrderByDescending(u => u.Id) as IEnumerable<Order>;
            
            var pageSize =int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            ViewBag.Name_User = _userService.GetById(userId).Name;
            return View(orders.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult FilterUser(int? page, string Model_Name)
        {
            var pageSize = int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            var listOrder = _orderService.GetAll(u => u.Status == (int) EntityStatus.Visible).OrderByDescending(u => u.Id) as IEnumerable<Order>;
           

            return PartialView("~/Areas/Admin/Views/Order/_partial_Search_Order.cshtml", listOrder.ToPagedList(pageNumber, pageSize));
        }
    }
}
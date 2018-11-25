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

namespace Web.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        // GET: Admin/Order
        public ActionResult ManageOrder(int? page)
        {
            var listOrder = _orderService.GetAll(u => u.Status == (int) EntityStatus.Visible).OrderByDescending(u => u.Id) as IEnumerable<Order>;
            var pageSize =int.Parse(WebConfigurationManager.AppSettings["PageSize"]);
            pageSize = pageSize == 0 ? 1 : pageSize;
            int pageNumber = page ?? 1;
            return View(listOrder.ToPagedList(pageNumber, pageSize));
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Helper.Sercurity;

namespace Web.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        [Helper.Sercurity.Authorize]
        public ActionResult Index()
        {
            var account = SessionPersister.AccountInformation;
            return View();
        }
    }
}
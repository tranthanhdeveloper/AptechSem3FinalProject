using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using Context.Database;
using PayPal.Api;
using Service.Service;
using Service.Service.Payment;
namespace Web.Controllers
{
    [Helper.Sercurity.Authorize]
    public class PaymentController : Controller
    {
        private string payorId;
        private ICourseService _courseService;
        private IOrderService _orderService;
        private IUserService _userService;
        private IPaymentService _paymentService;
        private IPaymentMethodService _paymentMethodService;
        private PaypalPaymentService _paypalPaymentService = new PaypalPaymentService();
        private APIContext _apiContext = PaypalConfiguration.GetAPIContext();
        public PaymentController(ICourseService courseService, IOrderService orderService, IUserService userService, IPaymentService paymentService, IPaymentMethodService paymentMethodService)
        {
            _courseService = courseService;
            _orderService = orderService;
            _userService = userService;
            _paymentService = paymentService;
            _paymentMethodService = paymentMethodService;
        }        

        public ActionResult PaymentWithPaypal(int id, string cancel = null)
        {            
            try
            {                
                var baseURI = GeneratePaymentUrl(out var guid);
                var courses = new List<Course>() {_courseService.GetById(id)};
                var createdPayment = _paypalPaymentService.CreatePayment(_apiContext, baseURI, courses, WebConfigurationManager.AppSettings["DefaultCurrency"]);
                var links = createdPayment.links.GetEnumerator();
                string paypalRedirectUrl = null;
                while (links.MoveNext())
                {
                    var lnk = links.Current;
                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        paypalRedirectUrl = lnk.href;
                    }
                }
                Session.Add(guid, createdPayment.id);
                return Redirect(paypalRedirectUrl);
            }
            catch (Exception exception)
            {
                return View("PaymentPaypalFailure");
            }
        }

        public ActionResult VerifyPaymentResult()
        {
            try
            {
                string payerId = Request.Params["PayerID"];
                var guid = Request.Params["guid"];
                var executedPayment = _paypalPaymentService.ExecutePayment(_apiContext, payerId, Session[guid] as string);
                if (executedPayment.state.ToLower() != "approved")
                {
                    return View("PaymentPaypalFailure");
                }
                var payment = new Context.Database.Payment
                {
                    PaymentMethod = _paymentMethodService.GetById(1), // Hard code for test this controller
                    PaymentStatus = 1,
                    CreatedDate = DateTime.Now,
                    User = _userService.GetAll().First(),
                };
                var savedPayment = _paymentService.Add(payment);

                var coursePaidId = int.Parse(executedPayment.transactions.First().item_list.items.First().sku);
                var order = new Context.Database.Order
                {
                    User = _userService.GetAll().First(),
                    Course = _courseService.GetById(coursePaidId),
                    Payment = savedPayment,
                    Status = 1,
                    CreatedDate = DateTime.Now,
                    
                };
                _orderService.Insert(order);
            }
            catch (Exception exception)
            {
                throw exception;
                //return View("PaymentPaypalFailure");
            }
            
            // handle code update data and redirect user to play page.
            return View("PaymentPaypalSuccess");
        }

        private string GeneratePaymentUrl(out string guid)
        {
            var baseUri = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/VerifyPaymentResult?";
            guid = Guid.NewGuid().ToString();
            return baseUri + "guid=" + guid;
        }
    }

}
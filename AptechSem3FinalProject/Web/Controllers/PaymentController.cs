using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Mvc;
using Context.Database;
using PayPal.Api;
using Service.Service;
using Service.Service.Payment;
namespace Web.Controllers
{
    public class PaymentController : Controller
    {
        private string payorId;
        private ICourseService _courseService;
        private PaypalPaymentService _paypalPaymentService = new PaypalPaymentService();
        private APIContext _apiContext = PaypalConfiguration.GetAPIContext();
        public PaymentController(ICourseService courseService)
        {
            _courseService = courseService;
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
            }
            catch (Exception)
            {
                return View("PaymentPaypalFailure");
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
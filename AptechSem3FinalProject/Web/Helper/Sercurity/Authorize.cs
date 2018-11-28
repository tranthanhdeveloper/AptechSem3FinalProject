using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Model.Enum;

namespace Web.Helper.Sercurity
{
    public class Authorize : AuthorizeAttribute
    {
        private readonly RoleEnum[] _types;

        public Authorize()
        {

        }
        public Authorize(params RoleEnum[] types)
        {
            _types = types;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            
            if (_types != null)
            {
                return _types.Any(AuthenticationManager.Is) || AuthenticationManager.IsAdmin;
            }
            return AuthenticationManager.IsAuthenticated;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            string area = filterContext.RouteData.DataTokens["area"].ToString();
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                var urlHelper = new UrlHelper(filterContext.RequestContext);
                filterContext.HttpContext.Response.StatusCode = 403;
                filterContext.Result = new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        message = "NotAuthorized",
                        LogOnUrl = urlHelper.Action("Login", "Account")
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                if (area == "Admin")
                {
                    filterContext.Result = RedirectToAction("Account", "login", area, HttpContext.Current.Request.RawUrl);
                }
                else if(area == "Client")
                {
                    filterContext.Result = RedirectToAction("home", "index", area, HttpContext.Current.Request.RawUrl);
                }
            }
        }

        private ActionResult RedirectToAction(string controller, string action, string area, string returnurl = "")
        {
            
            return new RedirectToRouteResult(new RouteValueDictionary(new { controller = controller, action = action, returnurl = returnurl, area = area }));
        }
    }
}
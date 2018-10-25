using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Context.Database;

namespace Web.Helper.Sercurity
{
    public class SessionPersister
    {
        public static Account AccountInformation
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }

                var session = HttpContext.Current.Session[SessionConstants.Account] as Account;
                return session;
            }
            set => HttpContext.Current.Session[SessionConstants.Account] = value;
        }

        public static Order OrderInfomation
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return null;
                }

                var session = HttpContext.Current.Session[SessionConstants.Order] as Order;
                return session;
            }
            set => HttpContext.Current.Session[SessionConstants.Order] = value;
        }
    }


}
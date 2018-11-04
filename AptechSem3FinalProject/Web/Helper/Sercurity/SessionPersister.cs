using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Context.Database;
using Model.Enum;

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

        public static bool IsAuthenticated => AccountInformation != null;

        public static bool IsAuthorized(RoleEnum type)

           => IsAuthenticated && AccountInformation.RoleId == (int)type;

        public static bool IsAdmin => IsAuthorized(RoleEnum.Admin);

        public static bool Is(RoleEnum type) => IsAuthorized(type);
    }


}
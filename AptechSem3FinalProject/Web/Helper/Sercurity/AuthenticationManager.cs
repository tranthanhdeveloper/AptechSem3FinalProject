using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Helper.Sercurity
{
    public class AuthenticationManager
    {
        public static int AccountId => SessionPersister.AccountInformation.Id;
    }
}
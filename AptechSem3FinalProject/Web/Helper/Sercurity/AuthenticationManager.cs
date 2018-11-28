using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Context.Database;
using Model.Enum;

namespace Web.Helper.Sercurity
{
    public class AuthenticationManager
    {
        public static bool IsAuthenticated => SessionPersister.IsAuthenticated;
        public static int AccountId => SessionPersister.AccountInformation.Id;
        public static bool IsAdmin => SessionPersister.IsAdmin;
        public static bool Is(RoleEnum type) => SessionPersister.Is(type);
        
        public static Account CurrentAccount => SessionPersister.AccountInformation;
        public static User CurrenUser => CurrentAccount.User;
    }
}
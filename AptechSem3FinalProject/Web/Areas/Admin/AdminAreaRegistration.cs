﻿using System.Web.Mvc;

namespace Web.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new {Controller="Account", action = "Login", id = UrlParameter.Optional },
                new[] { "Web.Areas.Admin.Controllers" }
            );
        }
    }
}
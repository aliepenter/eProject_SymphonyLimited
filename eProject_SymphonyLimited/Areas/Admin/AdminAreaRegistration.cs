using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin
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
              "Admin_login",
              "Admin/Login",
              new { Controller = "Auth", action = "Login", id = UrlParameter.Optional },
              new[] { "eProject_SymphonyLimited.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_logout",
                "Admin/Logout",
                new { Controller = "Auth", action = "Logout", id = UrlParameter.Optional },
                new[] { "eProject_SymphonyLimited.Areas.Admin.Controllers" }
            );

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
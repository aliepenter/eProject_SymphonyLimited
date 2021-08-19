using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Data
{
    [CustomizeAuthorize]
    public class BaseController : Controller
    {
        public BaseController()
        {
            if (System.Web.HttpContext.Current.Session["User"] == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/Login");
            }
        }
    }
}
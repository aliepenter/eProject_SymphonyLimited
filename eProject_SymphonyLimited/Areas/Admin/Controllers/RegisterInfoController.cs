using eProject_SymphonyLimited.Areas.Admin.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class RegisterInfoController : BaseController
    {
        // GET: Admin/RegisterInfo
        public ActionResult Index()
        {
            return View();
        }
    }
}
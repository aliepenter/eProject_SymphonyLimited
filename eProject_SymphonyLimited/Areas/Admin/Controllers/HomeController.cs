using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string account,string password)
        {
            var findUserId = db.EavEntity.Where(x => x.Name == "User").SingleOrDefault().EntityId;

            if (db.EavAttribute.Where(x => x.Name)
            {

            }
            if (users.Get(x => x.Email == email && x.Password == password) != null)
            {
                Session["user"] = users.Get(x => x.Email == email && x.Password == password).FirstOrDefault();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult NotFound404()
        {
            return View();
        }

        public ActionResult Tables()
        {
            return View();
        }
    }
}
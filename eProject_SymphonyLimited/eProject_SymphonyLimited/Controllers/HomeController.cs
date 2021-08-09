using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Controllers
{
    public class HomeController : Controller
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();
        public HomeController()
        {
            SymphonyLimitedDBContext db;
        }
        public ActionResult Index()
        {
            var cate = db.Category.Where(x => x.Level == 2).AsEnumerable();
            ViewBag.categoryLevel2 = cate;
            var subcate = db.Category.Where(x => x.Level == 3).AsEnumerable();
            ViewBag.categoryLevel3 = subcate;
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
            
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
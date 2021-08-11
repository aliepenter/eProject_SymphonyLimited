using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Controllers
{
    public class HomeController : Controller
    {
        SymphonyLimitedDBContext db;
        public HomeController()
        {
            db = new SymphonyLimitedDBContext();
            var cate = db.Category.Where(x => x.Level == 2).AsEnumerable();
            ViewBag.categoryLevel2 = cate;
            var subcate = db.Category.Where(x => x.Level == 3).AsEnumerable();
            ViewBag.categoryLevel3 = subcate;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult EntranceExam()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetChildCategory()
        {
            var childCategory = db.Category.Where(x => x.Level != 2 && x.Level != 1).AsEnumerable();
            return Json(new
            {
                data = childCategory
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Course()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStartTime()
        {
            var StartTime = db.Admission.AsEnumerable();
            return Json(new
            {
                data = StartTime
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
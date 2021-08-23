using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Controllers
{
    public class FaqController : Controller
    {
        SymphonyLimitedDBContext db;
        public FaqController()
        {
            db = new SymphonyLimitedDBContext();
        }
        // GET: Faq
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult getFaq()
        {
            var faq = db.Faq.AsEnumerable();
            string jsonData = JsonConvert.SerializeObject(faq);
            return Json(new
            {
                data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
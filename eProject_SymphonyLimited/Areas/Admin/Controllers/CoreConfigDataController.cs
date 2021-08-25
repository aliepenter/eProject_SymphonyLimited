using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class CoreConfigDataController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/CoreConfigData
        public ActionResult Index()
        {
            return View(db.CoreConfigData.AsEnumerable());
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
            var coreConfigDatas = db.CoreConfigData.AsEnumerable();
            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "Name":
                        coreConfigDatas = db.CoreConfigData.Where(x => x.Name.Contains(key)).AsEnumerable();
                        break;
                    case "Value":
                        coreConfigDatas = db.CoreConfigData.Where(x => x.Value.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)coreConfigDatas.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(coreConfigDatas.Skip((page - 1) * pageSize).Take(pageSize));
            return Json(new
            {
                TotalPages = totalPages,
                CurrentPage = page,
                StatusCode = 200,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Edit(int id)
        {
            var coreConfigData = db.CoreConfigData.FirstOrDefault(x => x.EntityId == id);
            if (coreConfigData != null)
            {
                return View(coreConfigData);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CoreConfigData ccd)
        {
            var currentCoreConfigData = db.CoreConfigData.Find(ccd.EntityId);
            if (currentCoreConfigData.Code == "maximum_student_in_class" || currentCoreConfigData.Code == "minium_student_in_class")
            {
                var isInt = Int32.TryParse(ccd.Value, out int number);
                if (!isInt)
                {
                    ModelState.AddModelError("Value", "Data must be integer!");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    currentCoreConfigData.Value = ccd.Value;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save coreconfigdata!");
                }
            }
            return View(currentCoreConfigData);
        }
    }
}
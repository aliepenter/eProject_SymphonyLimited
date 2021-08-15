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

        public ActionResult FindId(int id)
        {
            return Json(db.CoreConfigData.Find(id), JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public ActionResult DeleteData(int id)
        {
            if (ModelState.IsValid)
            {
                var findCoreConfigData = db.CoreConfigData.Find(id);
                if (findCoreConfigData != null)
                {
                    db.CoreConfigData.Remove(findCoreConfigData);
                    db.SaveChanges();
                    return Json(new
                    {
                        statusCode = 200,
                        message = "Xóa thành công!",
                        data = id,
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        statusCode = 403,
                        message = "Xóa thất bại!",
                        data = id,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                statusCode = 403,
                message = "Xóa thất bại!",
                data = id,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CoreConfigData ccd)
        {
            if (ModelState.IsValid)
            {
                var validateName = db.CoreConfigData.FirstOrDefault(x => x.Name == ccd.Name);
                if (validateName == null)
                {
                    try
                    {
                        ccd.Code = ccd.Name.ToLower().Replace(" ", "_");
                        db.CoreConfigData.Add(ccd);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Some thing went wrong while save coreconfigdata!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "CoreConfigData Name is already exist!");
                }
            }
            return View();
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
            if (ModelState.IsValid)
            {
                var currentCoreConfigData = db.CoreConfigData.Find(ccd.EntityId);
                var validateName = db.CoreConfigData.FirstOrDefault(x => x.Name != currentCoreConfigData.Name && x.Name == ccd.Name);
                if (validateName == null)
                {
                    try
                    {
                        currentCoreConfigData.Name = ccd.Name;
                        currentCoreConfigData.Code = ccd.Name.ToLower().Replace(" ", "_");
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Some thing went wrong while save coreconfigdata!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "CoreConfigData Name is already exist!");
                }
            }
            return View();
        }
    }
}
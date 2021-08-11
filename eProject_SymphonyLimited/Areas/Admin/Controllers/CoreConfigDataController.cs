using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
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
                try
                {
                    var coreConfigData = db.CoreConfigData.FirstOrDefault(x => x.EntityId == ccd.EntityId);
                    db.CoreConfigData.Add(ccd);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

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
                try
                {
                    db.Entry(ccd).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                }
            }
            return View();
        }
    }
}
using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class CoreConfigDataController : Controller
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/CoreConfigData
        public ActionResult Index()
        {
            return View(db.CoreConfigData.AsEnumerable());
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
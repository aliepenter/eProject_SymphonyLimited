using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class BranchController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/CoreConfigData
        public ActionResult Index()
        {
            return View(db.Branch.AsEnumerable());
        }

        public ActionResult FindId(int id)
        {
            return Json(db.Branch.Find(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteData(int id)
        {
            if (ModelState.IsValid)
            {
                var findBranch = db.Branch.Find(id);
                if (findBranch != null)
                {
                    db.Branch.Remove(findBranch);
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
        public ActionResult Create(Branch b)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var branch = db.Branch.FirstOrDefault(x => x.EntityId == b.EntityId);
                    db.Branch.Add(b);
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
            var branch = db.Branch.FirstOrDefault(x => x.EntityId == id);
            if (branch != null)
            {
                return View(branch);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Branch b)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(b).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                }
            }
            return View();
        }

        public ActionResult Details(int id)
        {
            return View(db.Branch.Find(id));
        }
    }
}
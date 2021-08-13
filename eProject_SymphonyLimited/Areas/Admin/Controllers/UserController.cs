using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using eProject_SymphonyLimited.Models.Authorize;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/CoreConfigData
        public ActionResult Index()
        {
            return View(db.User.AsEnumerable());
        }

        public ActionResult FindId(int id)
        {
            return Json(db.User.Find(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteData(int id)
        {
            if (ModelState.IsValid)
            {
                var findUser = db.User.Find(id);
                if (findUser != null)
                {
                    db.User.Remove(findUser);
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
        public ActionResult Create(User u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = db.User.FirstOrDefault(x => x.EntityId == u.EntityId);
                    db.User.Add(u);
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
            var user = db.User.FirstOrDefault(x => x.EntityId == id);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(User u)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(u).State = EntityState.Modified;
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
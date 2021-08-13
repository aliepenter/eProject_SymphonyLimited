using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using eProject_SymphonyLimited.Models.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers.Authorize
{
    public class BusinessController : Controller
    {
        SymphonyLimitedDBContext db;

        public BusinessController()
        {
            db = new SymphonyLimitedDBContext();
        }

        // GET: Admin/Business
        public ActionResult Index()
        {
            var data = db.Business.AsEnumerable();
            return View(data);
        }

        public ActionResult Update()
        {
            // Lấy các controllers trong Admin
            var controllers = Helpers.GetController("eProject_SymphonyLimited.Areas.Admin.Controllers");
            // Lưu vào DB
            foreach (var item in controllers)
            {
                Business b = new Business();
                b.EntityId = item.Name;
                b.Name = item.Name.Replace("Controller", "");
                if (!db.Business.AsNoTracking().Any(x => x.EntityId.Equals(b.EntityId)))
                {
                    db.Business.Add(b);
                    db.SaveChanges();
                }
                // Lấy các Action (Permission) trong controller đó lưu db
                var acts = Helpers.GetAction(item);
                foreach (var act in acts)
                {
                    Permission p = new Permission();
                    // ProductController-Index
                    p.Name = item.Name + "-" + act;
                    p.Description = act;
                    p.BusinessId = item.Name;
                    if (!db.Permission.AsNoTracking().Any(x => x.Name.Equals(p.Name)))
                    {
                        db.Permission.Add(p);
                        db.SaveChanges();
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}
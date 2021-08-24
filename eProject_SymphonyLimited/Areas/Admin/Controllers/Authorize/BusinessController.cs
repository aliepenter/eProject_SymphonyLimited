using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using eProject_SymphonyLimited.Models.Authorize;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers.Authorize
{
    [CustomizeAuthorize]
    public class BusinessController : BaseController
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
        public ActionResult FindId(int id)
        {
            return Json(db.Business.Find(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
            var businesses = db.Business.AsEnumerable();
            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
                        businesses = db.Business.Where(x => x.EntityId.Contains(key)).AsEnumerable();
                        break;
                    case "Name":
                        businesses = db.Business.Where(x => x.Name.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)businesses.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(businesses.Skip((page - 1) * pageSize).Take(pageSize));
            return Json(new
            {
                TotalPages = totalPages,
                CurrentPage = page,
                StatusCode = 200,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Update()
        {
            // Lấy các controllers trong Admin
            var controllers = Helpers.GetController("eProject_SymphonyLimited.Areas.Admin.Controllers");
            // Lưu vào DB
            foreach (var item in controllers)
            {
                if (!item.Name.Equals("AuthController"))
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
            }
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string id)
        {
            var business = db.Business.FirstOrDefault(x => x.EntityId == id);
            if (business != null)
            {
                return View(business);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Business b)
        {
            var currentBusiness = db.Business.Find(b.EntityId);
            var validateName = db.Business.FirstOrDefault(x => x.Name != currentBusiness.Name && x.Name == b.Name);
            if (validateName != null)
            {
                ModelState.AddModelError("Name", "Business name can't be the same!");
            }
            if (ModelState.IsValid)
            {

                try
                {
                    currentBusiness.Name = b.Name;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save Business!");
                }
            }
            return View();
        }
    }
}
using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class ClassController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
            var classes = db.Class.AsEnumerable().Select(x => new ClassViewModel(x));
            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "Name":
                        classes = db.Class.AsEnumerable().Select(x => new ClassViewModel(x)).Where(x => x.Name.Contains(key));
                        break;
                    case "AdmissionName":
                        classes = db.Class.AsEnumerable().Select(x => new ClassViewModel(x)).Where(x => x.AdmissionName.Contains(key));
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)classes.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(classes.Skip((page - 1) * pageSize).Take(pageSize));
            return Json(new
            {
                TotalPages = totalPages,
                CurrentPage = page,
                StatusCode = 200,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            ViewBag.AdmissionId = new SelectList(db.Admission.AsEnumerable(), "EntityId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Class c)
        {
            ViewBag.AdmissionId = new SelectList(db.Admission.AsEnumerable(), "EntityId", "Name", c.AdmissionId);
            if (ModelState.IsValid)
            {
                var validateName = db.Class.FirstOrDefault(x => x.Name == c.Name);
                if (validateName == null)
                {
                    try
                    {
                        c.QuantityStudent = 0;
                        c.Status = false;
                        db.Class.Add(c);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Some thing went wrong while save class!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "Class Name is already exist!");
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var _class = db.Class.Find(id);
            if (_class != null)
            {
                return View(_class);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Class c)
        {
            if (ModelState.IsValid)
            {
                var currentClass = db.Class.FirstOrDefault(x => x.EntityId == c.EntityId);
                var validateName = db.Class.FirstOrDefault(x => x.Name != currentClass.Name && x.Name == c.Name);
                if (validateName == null)
                {
                    try
                    {
                        //Todo
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Some thing went wrong while save class!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "Class Name is already exist!");
                }
            }
            return View();
        }
    }
}
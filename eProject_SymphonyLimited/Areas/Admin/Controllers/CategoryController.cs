using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
            var categories = db.Category.Where(x => x.ParentId != 0).AsEnumerable();
            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
                        categories = db.Category.Where(x => x.ParentId != 0 && x.EntityId.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "ParentId":
                        categories = db.Category.Where(x => x.ParentId != 0 && x.ParentId.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Name":
                        categories = db.Category.Where(x => x.ParentId != 0 && x.Name.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)categories.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(categories.Skip((page - 1) * pageSize).Take(pageSize));
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
            ViewBag.ParentList = db.Category.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category c)
        {
            if (ModelState.IsValid)
            {
                var validateName = db.Category.FirstOrDefault(x => x.Name == c.Name);
                if (validateName == null)
                {
                    try
                    {
                        var categoryById = db.Category.FirstOrDefault(x => x.EntityId == c.ParentId);
                        if (categoryById != null)
                        {
                            if (categoryById.Level == 1)
                            {
                                c.Path = categoryById.Path + "/";
                            }
                            else
                            {
                                c.Path = categoryById.Path + categoryById.EntityId + "/";
                            }
                            c.Level = categoryById.Level + 1;
                            db.Category.Add(c);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Some thing went wrong while save category!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "Category is already exist!");
                }

            }
            ViewBag.ParentList = db.Category.ToList();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var categoryById = db.Category.FirstOrDefault(x => x.EntityId == id);
            if (categoryById != null)
            {
                ViewBag.ParentList = db.Category.Where(x => x.EntityId != id && !x.Path.Contains(id.ToString())).ToList();
                return View(categoryById);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Category c)
        {
            var currentCategory = db.Category.FirstOrDefault(x => x.EntityId == c.EntityId);
            var validateName = db.Category.FirstOrDefault(x => x.Name != currentCategory.Name && x.Name == c.Name);
            if (validateName != null)
            {
                ModelState.AddModelError("Name", "Category is already exist!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var parentCategory = db.Category.FirstOrDefault(x => x.EntityId == c.ParentId);
                    var childCategories = db.Category.Where(x => x.Path.Contains("/" + c.EntityId.ToString() + "/")).AsEnumerable();
                    var mainCategory = db.Category.Find(c.EntityId);
                    var oldPath = mainCategory.Path;
                    if (parentCategory != null)
                    {
                        if (parentCategory.Level == 1)
                        {
                            mainCategory.Path = parentCategory.Path + "/";
                        }
                        else
                        {
                            mainCategory.Path = parentCategory.Path + parentCategory.EntityId + "/";
                        }
                        foreach (var child in childCategories)
                        {
                            child.Level = parentCategory.Level + (child.Level - mainCategory.Level) + 1;
                            child.Path = child.Path.Replace(oldPath, mainCategory.Path);
                        }
                        mainCategory.Name = c.Name;
                        mainCategory.Level = parentCategory.Level + 1;
                        mainCategory.ParentId = parentCategory.EntityId;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save category!");
                }
            }
            ViewBag.ParentList = db.Category.Where(x => x.ParentId != c.EntityId).ToList();
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (db.Category.Find(id) != null)
            {
                try
                {
                    var childCategories = db.Category.Where(x => x.Path.Contains("/" + id.ToString() + "/")).AsEnumerable();
                    foreach (var item in childCategories)
                    {
                        db.Category.Remove(db.Category.Find(item.EntityId));
                    }
                    db.Category.Remove(db.Category.Find(id));
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while delete category!");
                }
            }
            return View();
        }
    }
}
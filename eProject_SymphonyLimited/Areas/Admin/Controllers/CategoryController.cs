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
    public class CategoryController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(db.Category.Where(x => x.ParentId != 0).AsEnumerable());
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
                        mainCategory.Level = parentCategory.Level + 1;
                        mainCategory.ParentId = parentCategory.EntityId;
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

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

                }
            }
            return View();
        }
    }
}
using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class CourseController : Controller
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Course
        public ActionResult Index()
        {
            ViewBag.Courses = db.Course.Join(db.Category,
                co => co.CategoryId,
                ca => ca.EntityId,
                (co, ca) => new
                CourseViewModel
                {
                    EntityId = co.EntityId,
                    Name = co.Name,
                    Price = co.Price,
                    Subject = co.Subject,
                    Certificate = co.Certificate,
                    Image = co.Image,
                    Description = co.Description,
                    Time = co.Time,
                    Category = ca.Name
                }).AsEnumerable();
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.CategoryList = db.Category.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course c, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string imgName = Path.GetFileName(imgFile.FileName);
                    string imgText = Path.GetExtension(imgName);
                    string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                    c.Image = "~/Areas/Admin/Content/img/" + imgName;
                    if (imgFile.ContentLength > 0)
                    {
                        db.Course.Add(c);
                        string oldImagePath = Request.MapPath(Session["imgPath"].ToString());
                        if (db.SaveChanges() > 0)
                        {
                            imgFile.SaveAs(imgPath);
                            ViewBag.msg = "Record Added";
                            ModelState.Clear();
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ViewBag.msg = "Hello";
                }
            }
            ViewBag.CategoryList = db.Category.ToList();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var courseById = db.Course.FirstOrDefault(x => x.EntityId == id);
            if (courseById != null)
            {
                Session["imgPath"] = courseById.Image;
                if (courseById.Image == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryList = db.Category.ToList();
                return View(courseById);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Course c, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (imgFile != null)
                    {
                        string imgName = Path.GetFileName(imgFile.FileName);
                        string imgText = Path.GetExtension(imgName);
                        string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                        c.Image = "~/Areas/Admin/Content/img/" + imgName;
                        if (imgFile.ContentLength > 0)
                        {
                            db.Entry(c).State = EntityState.Modified;
                            string oldImagePath = Request.MapPath(Session["imgPath"].ToString());
                            if (db.SaveChanges() > 0)
                            {
                                imgFile.SaveAs(imgPath);
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                                TempData["msg"] = "Record Updated";
                                //ViewBag.msg = "Record Added";
                                //ModelState.Clear();
                            }
                        }
                        
                    }
                    else
                    {
                        c.Image = Session["imgPath"].ToString();
                        db.Entry(c).State = EntityState.Modified;
                        if (db.SaveChanges()> 0)
                        {
                            TempData["msg"] = "Data Updated";
                            
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                }
            }
            ViewBag.CategoryList = db.Category.ToList();
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (db.Course.Find(id) != null)
            {
                try
                {
                    db.Course.Remove(db.Course.Find(id));
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
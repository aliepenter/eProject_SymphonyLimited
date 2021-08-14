using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using eProject_SymphonyLimited.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class CourseController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Course
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
            var courses = db.Course.Join(db.Category,
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
            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
                        courses = db.Course.Join(db.Category,
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
                }).Where(x => x.EntityId.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Price":
                        courses = db.Course.Join(db.Category,
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
                }).Where(x => x.Price.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Description":
                        courses = db.Course.Join(db.Category,
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
                }).Where(x => x.Description.Contains(key)).AsEnumerable();
                        break;
                    case "Name":
                        courses = db.Course.Join(db.Category,
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
                }).Where(x => x.Name.Contains(key)).AsEnumerable();
                        break;
                    case "Subject":
                        courses = db.Course.Join(db.Category,
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
                }).Where(x => x.Subject.Contains(key)).AsEnumerable();
                        break;
                    case "Time":
                        courses = db.Course.Join(db.Category,
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
                }).Where(x => x.Time.Contains(key)).AsEnumerable();
                        break;
                    case "Certificate":
                        courses = db.Course.Join(db.Category,
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
                }).Where(x => x.Certificate.Contains(key)).AsEnumerable();
                        break;
                    case "Category":
                        courses = db.Course.Join(db.Category,
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
                }).Where(x => x.Category.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)courses.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(courses.Skip((page - 1) * pageSize).Take(pageSize));
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
            var categoryCollection = db.Category.AsEnumerable();
            if (categoryCollection.Count() <= 1)
            {
                TempData["ErrorMessage"] = "Please create category before create course!";
                return RedirectToAction("Index");
            }
            ViewBag.CategoryList = db.Category.Where(x => x.ParentId != 0).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course c, HttpPostedFileBase imgFile)
        {
            var validateName = db.Course.FirstOrDefault(x => x.Name == c.Name);
            if (validateName != null)
            {
                ModelState.AddModelError("Name", "Course name can't be the same!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (imgFile == null && c.Image == null)
                    {
                        c.Image = "default.png";
                        db.Course.Add(c);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string imgName = Path.GetFileName(imgFile.FileName);
                        string imgText = Path.GetExtension(imgName);
                        string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                        c.Image = imgName;
                        if (imgFile.ContentLength > 0)
                        {
                            db.Course.Add(c);
                            if (db.SaveChanges() > 0)
                            {
                                imgFile.SaveAs(imgPath);
                                ViewBag.msg = "Record Added";
                                ModelState.Clear();
                            }
                        }
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save course!");
                }
            }
            ViewBag.CategoryList = db.Category.Where(x => x.ParentId != 0).ToList();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var courseById = db.Course.FirstOrDefault(x => x.EntityId == id);
            if (courseById != null)
            {
                if (courseById.Image == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryList = db.Category.Where(x => x.ParentId != 0).ToList();
                return View(courseById);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Course c, HttpPostedFileBase imgFile)
        {
            var currentCourse = db.Course.Find(c.EntityId);
            var validateName = db.Course.FirstOrDefault(x => x.Name != currentCourse.Name && x.Name == c.Name);
            if (validateName != null)
            {
                ModelState.AddModelError("Name", "Course name can't be the same!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    currentCourse.Name = c.Name;
                    currentCourse.Price = c.Price;
                    currentCourse.Subject = c.Subject;
                    currentCourse.Time = c.Time;
                    currentCourse.Certificate = c.Certificate;
                    currentCourse.CategoryId = c.CategoryId;
                    currentCourse.Description = c.Description;
                    if (imgFile != null)
                    {
                        string imgName = Path.GetFileName(imgFile.FileName);
                        string imgText = Path.GetExtension(imgName);
                        string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                        currentCourse.Image = imgName;
                        if (imgFile.ContentLength > 0)
                        {
                            if (db.SaveChanges() > 0)
                            {
                                imgFile.SaveAs(imgPath);
                            }
                        }
                    }
                    else
                    {
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save course!");
                }
            }
            ViewBag.CategoryList = db.Category.Where(x => x.ParentId != 0).ToList();
            return View(currentCourse);
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
                    ModelState.AddModelError("", "Some thing went wrong while delete course!");
                }
            }
            return View();
        }
    }
}
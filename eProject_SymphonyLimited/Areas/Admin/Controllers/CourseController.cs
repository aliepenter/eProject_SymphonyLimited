using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using eProject_SymphonyLimited.Models.ViewModel;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
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
        public ActionResult Get(int page = 1, string key = null)
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
            if (!String.IsNullOrEmpty(key))
            {
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
            ViewBag.CategoryList = db.Category.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Course.Add(c);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

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
                ViewBag.CategoryList = db.Category.ToList();
                return View(courseById);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Course c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
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
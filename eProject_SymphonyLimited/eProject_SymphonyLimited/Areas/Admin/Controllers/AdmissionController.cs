using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class AdmissionController : Controller
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Admission
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string key = null)
        {
            int pageSize = 5;
            var admissions = db.Admission.Join(db.Course,
                a => a.CourseId,
                c => c.EntityId,
                (a, c) => new
                AdmissionViewModel
                {
                    EntityId = a.EntityId,
                    Name = a.Name,
                    Price = a.Price,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    QuantityStudent = a.QuantityStudent,
                    MarkPass = a.MarkPass,
                    Course = c.Name
                }).AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                admissions = db.Admission.Join(db.Course,
                a => a.CourseId,
                c => c.EntityId,
                (a, c) => new
                AdmissionViewModel
                {
                    EntityId = a.EntityId,
                    Name = a.Name,
                    Price = a.Price,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    QuantityStudent = a.QuantityStudent,
                    MarkPass = a.MarkPass,
                    Course = c.Name
                }).Where(x => x.Name.Contains(key)).AsEnumerable();
            }
            decimal totalPages = Math.Ceiling((decimal)admissions.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(admissions.Skip((page - 1) * pageSize).Take(pageSize));
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
            var courseCollection = db.Course.AsEnumerable();
            if (courseCollection.Count() == 0)
            {
                TempData["ErrorMessage"] = "Please create course before create admission!";
                return RedirectToAction("Index", new { ErrorMessage = "Please create course before create admission!" });
            }
            ViewBag.CourseList = db.Course.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Admission a)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Admission.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                }
            }
            ViewBag.CourseList = db.Course.ToList();
            return View();
        }

        public ActionResult Edit(int id)
        {
            var admissionById = db.Admission.FirstOrDefault(x => x.EntityId == id);
            if (admissionById != null)
            {
                ViewBag.CourseList = db.Course.ToList();
                return View(admissionById);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Admission a)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(a).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

                }
            }
            ViewBag.CourseList = db.Course.ToList();
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (db.Admission.Find(id) != null)
            {
                try
                {
                    db.Admission.Remove(db.Admission.Find(id));
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
using eProject_SymphonyLimited.Models;
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
            ViewBag.Admissions = db.Admission.Join(db.Course,
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
            return View();
        }

        public ActionResult Create()
        {
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
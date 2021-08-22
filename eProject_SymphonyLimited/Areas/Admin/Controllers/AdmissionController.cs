using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class AdmissionController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Admission
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
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
                    Course = c.Name
                }).AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
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
                    Course = c.Name
                }).Where(x => x.EntityId.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Name":
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
                    Course = c.Name
                }).Where(x => x.Name.Contains(key)).AsEnumerable();
                        break;
                    case "StartTime":
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
                    Course = c.Name
                }).Where(x => x.StartTime.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "EndTime":
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
                    Course = c.Name
                }).Where(x => x.EndTime.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Price":
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
                    Course = c.Name
                }).Where(x => x.Price.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "QuantityStudent":
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
                    Course = c.Name
                }).Where(x => x.QuantityStudent.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Course":
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
                    Course = c.Name
                }).Where(x => x.Course.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
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
                return RedirectToAction("Index");
            }
            ViewBag.CourseList = db.Course.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Admission a)
        {
            var validateName = db.Admission.FirstOrDefault(x => x.Name == a.Name);
            var admission = db.Admission.Where(x => x.CourseId == a.CourseId).AsEnumerable();
            if (a.StartTime <= DateTime.Now)
            {
                ModelState.AddModelError("StartTime", "Start time must bigger than current time!");
            }
            if (a.EndTime <= DateTime.Now)
            {
                ModelState.AddModelError("EndTime", "End time must bigger than current time!");
            }
            if (a.StartTime >= a.EndTime)
            {
                ModelState.AddModelError("EndTime", "End time must bigger than start time!");
            }
            if (validateName != null)
            {
                ModelState.AddModelError("Name", "Admission name can't be the same!");
            }
            foreach (var item in admission)
            {
                if (a.StartTime < item.StartTime)
                {
                    if (a.EndTime > item.StartTime)
                    {
                        ModelState.AddModelError("EndTime", "End time is already in another admission!");
                    }
                    if (a.EndTime > item.EndTime)
                    {
                        ModelState.AddModelError("StartTime", "Start time is already in another admission!");
                        ModelState.AddModelError("EndTime", "End time is already in another admission!");
                    }
                }
                if (item.StartTime <= a.StartTime && a.StartTime < item.EndTime)
                {
                    ModelState.AddModelError("", "Time is already in another admission!");
                }
            }
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
                    ModelState.AddModelError("", "Some thing went wrong while save admission!");
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
                if (admissionById.EndTime < DateTime.Now)
                {
                    TempData["ErrorMessage"] = "Admission Ended!";
                }
                else
                {
                    ViewBag.CourseList = db.Course.ToList();
                    return View(admissionById);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Admission a)
        {
            var currentAdmission = db.Admission.Find(a.EntityId);
            var admission = db.Admission.Where(x => x.EntityId != currentAdmission.EntityId && x.CourseId == a.CourseId).AsEnumerable();
            var validateName = db.Admission.FirstOrDefault(x => x.Name != currentAdmission.Name && x.Name == a.Name);
            if (a.StartTime <= DateTime.Now)
            {
                ModelState.AddModelError("StartTime", "Start time must bigger than current time!");
            }
            if (a.EndTime <= DateTime.Now)
            {
                ModelState.AddModelError("EndTime", "End time must bigger than current time!");
            }
            if (a.StartTime >= a.EndTime)
            {
                ModelState.AddModelError("EndTime", "End time must bigger than start time!");
            }
            if (validateName != null)
            {

                ModelState.AddModelError("Name", "Admission name can't be the same!");
            }
            foreach (var item in admission)
            {
                if (a.StartTime < item.StartTime)
                {
                    if (a.EndTime > item.StartTime)
                    {
                        ModelState.AddModelError("EndTime", "End time is already in another admission!");
                    }
                    if (a.EndTime > item.EndTime)
                    {
                        ModelState.AddModelError("StartTime", "Start time is already in another admission!");
                        ModelState.AddModelError("EndTime", "End time is already in another admission!");
                    }
                }
                if (item.StartTime <= a.StartTime && a.StartTime < item.EndTime)
                {
                    ModelState.AddModelError("", "Time is already in another admission!");
                }
            }
            if (ModelState.IsValid)
            {
                try
                {
                    currentAdmission.Name = a.Name;
                    currentAdmission.StartTime = a.StartTime;
                    currentAdmission.EndTime = a.EndTime;
                    currentAdmission.QuantityStudent = a.QuantityStudent;
                    currentAdmission.Price = a.Price;
                    currentAdmission.CourseId = a.CourseId;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save admission!");
                }
            }
            ViewBag.CourseList = db.Course.ToList();
            return View();
        }

        public ActionResult Delete(int id)
        {
            var admissionById = db.Admission.FirstOrDefault(x => x.EntityId == id);
            if (db.Admission.Find(id) != null)
            {
                if (admissionById.EndTime < DateTime.Now)
                {
                    TempData["ErrorMessage"] = "Can't delete this admission!";
                    return RedirectToAction("Index");
                }
                else
                {
                    try
                    {
                        db.Admission.Remove(db.Admission.Find(id));
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Some thing went wrong while save admission!");
                    }
                }
            }
            return View();
        }
    }
}
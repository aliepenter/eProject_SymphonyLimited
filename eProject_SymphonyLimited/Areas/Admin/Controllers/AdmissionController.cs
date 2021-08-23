﻿using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            List<AdmissionViewModel> admissionModel = new List<AdmissionViewModel>();
            foreach (var item in db.Admission.Include(x => x.Course))
            {
                admissionModel.Add(new AdmissionViewModel
                {
                    EntityId = item.EntityId,
                    Name = item.Name,
                    Price = item.Price,
                    StartTime = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                    EndTime = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                    BillTime = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                    QuantityStudent = item.QuantityStudent,
                    Course = item.Course.Name
                });
            }
            if (!String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
                        foreach (var item in db.Admission.Include(x => x.Course).Where(x => x.EntityId.ToString().Contains(key)))
                        {
                            admissionModel.Add(new AdmissionViewModel
                            {
                                EntityId = item.EntityId,
                                Name = item.Name,
                                Price = item.Price,
                                StartTime = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                                EndTime = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                                BillTime = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                                QuantityStudent = item.QuantityStudent,
                                Course = item.Course.Name
                            });
                        }
                        break;
                    case "Name":
                        foreach (var item in db.Admission.Include(x => x.Course).Where(x => x.Name.ToString().Contains(key)))
                        {
                            admissionModel.Add(new AdmissionViewModel
                            {
                                EntityId = item.EntityId,
                                Name = item.Name,
                                Price = item.Price,
                                StartTime = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                                EndTime = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                                BillTime = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                                QuantityStudent = item.QuantityStudent,
                                Course = item.Course.Name
                            });
                        }
                        break;
                    case "StartTime":
                        foreach (var item in db.Admission.Include(x => x.Course).Where(x => x.StartTime.ToString().Contains(key)))
                        {
                            admissionModel.Add(new AdmissionViewModel
                            {
                                EntityId = item.EntityId,
                                Name = item.Name,
                                Price = item.Price,
                                StartTime = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                                EndTime = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                                BillTime = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                                QuantityStudent = item.QuantityStudent,
                                Course = item.Course.Name
                            });
                        }
                        break;
                    case "EndTime":
                        foreach (var item in db.Admission.Include(x => x.Course).Where(x => x.EndTime.ToString().Contains(key)))
                        {
                            admissionModel.Add(new AdmissionViewModel
                            {
                                EntityId = item.EntityId,
                                Name = item.Name,
                                Price = item.Price,
                                StartTime = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                                EndTime = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                                BillTime = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                                QuantityStudent = item.QuantityStudent,
                                Course = item.Course.Name
                            });
                        }
                        break;
                    case "BillTime":
                        foreach (var item in db.Admission.Include(x => x.Course).Where(x => x.BillTime.ToString().Contains(key)))
                        {
                            admissionModel.Add(new AdmissionViewModel
                            {
                                EntityId = item.EntityId,
                                Name = item.Name,
                                Price = item.Price,
                                StartTime = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                                EndTime = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                                BillTime = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                                QuantityStudent = item.QuantityStudent,
                                Course = item.Course.Name
                            });
                        }
                        break;
                    case "Price":
                        foreach (var item in db.Admission.Include(x => x.Course).Where(x => x.Price.ToString().Contains(key)))
                        {
                            admissionModel.Add(new AdmissionViewModel
                            {
                                EntityId = item.EntityId,
                                Name = item.Name,
                                Price = item.Price,
                                StartTime = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                                EndTime = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                                BillTime = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                                QuantityStudent = item.QuantityStudent,
                                Course = item.Course.Name
                            });
                        }
                        break;
                    case "QuantityStudent":
                        foreach (var item in db.Admission.Include(x => x.Course).Where(x => x.QuantityStudent.ToString().Contains(key)))
                        {
                            admissionModel.Add(new AdmissionViewModel
                            {
                                EntityId = item.EntityId,
                                Name = item.Name,
                                Price = item.Price,
                                StartTime = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                                EndTime = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                                BillTime = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                                QuantityStudent = item.QuantityStudent,
                                Course = item.Course.Name
                            });
                        }
                        break;
                    case "Course":
                        foreach (var item in db.Admission.Include(x => x.Course).Where(x => x.Course.ToString().Contains(key)))
                        {
                            admissionModel.Add(new AdmissionViewModel
                            {
                                EntityId = item.EntityId,
                                Name = item.Name,
                                Price = item.Price,
                                StartTime = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                                EndTime = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                                BillTime = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                                QuantityStudent = item.QuantityStudent,
                                Course = item.Course.Name
                            });
                        }
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)admissionModel.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(admissionModel.Skip((page - 1) * pageSize).Take(pageSize));
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
            if (a.BillTime < a.EndTime)
            {
                ModelState.AddModelError("BillTime", "Bill time must bigger than end time!");
            }
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
            if (a.BillTime < a.EndTime)
            {
                ModelState.AddModelError("BillTime", "Bill time must bigger than end time!");
            }
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
                    currentAdmission.BillTime = a.BillTime;
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
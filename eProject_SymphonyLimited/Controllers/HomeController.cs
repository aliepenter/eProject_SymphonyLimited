﻿using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Controllers
{
    public class HomeController : Controller
    {
        SymphonyLimitedDBContext db;
        public HomeController()
        {
            db = new SymphonyLimitedDBContext();
            var cate = db.Category.Where(x => x.Level == 2).AsEnumerable();
            ViewBag.categoryLevel2 = cate;
            var subcate = db.Category.Where(x => x.Level == 3).AsEnumerable();
            ViewBag.categoryLevel3 = subcate;
            var admission = db.Admission.AsEnumerable();
            ViewBag.admission = admission;
            var teacher = db.Teacher.AsEnumerable();
            ViewBag.admissionList = db.Admission.ToList();
            ViewBag.teacher = teacher;
            ViewBag.phoneInFooter = db.CoreConfigData.FirstOrDefault(x => x.Code == "phone_in_footer");
            ViewBag.emailInFooter = db.CoreConfigData.FirstOrDefault(x => x.Code == "email_in_footer");
            ViewBag.addressInFooter = db.CoreConfigData.FirstOrDefault(x => x.Code == "address_in_footer");

        }

        public ActionResult GetChildCategories()
        {

            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            var branch = db.Branch.AsEnumerable();
            ViewBag.branch = branch;
            return View();
        }
        public ActionResult EntranceExam()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetChildCategory()
        {
            var childCategory = db.Category.Where(x => x.Level != 2 && x.Level != 1).AsEnumerable();
            return Json(new
            {
                data = childCategory
            }, JsonRequestBehavior.AllowGet);
        }

        public List<Course> GetCoursesByCondition(int page, string sorter, string orderBy, int limiter, int id = 0)
        {
            List<Course> courses = null;
            if (id == 0)
            {
                switch (sorter)
                {
                    case "name":
                        if (orderBy == "asc")
                        {
                            courses = db.Course.OrderBy(x => x.Name).Skip((page - 1) * limiter).Take(limiter).ToList();
                        }
                        else if (orderBy == "desc")
                        {
                            courses = db.Course.OrderByDescending(x => x.Name).Skip((page - 1) * limiter).Take(limiter).ToList();
                        }
                        break;
                    case "price":
                        if (orderBy == "asc")
                        {
                            courses = db.Course.OrderBy(x => x.Price).Skip((page - 1) * limiter).Take(limiter).ToList();
                        }
                        else if (orderBy == "desc")
                        {
                            courses = db.Course.OrderByDescending(x => x.Price).Skip((page - 1) * limiter).Take(limiter).ToList();
                        }
                        break;
                    default:
                        courses = db.Course.Skip((page - 1) * limiter).Take(limiter).ToList();
                        break;
                }
            }
            else
            {
                switch (sorter)
                {
                    case "name":
                        if (orderBy == "asc")
                        {
                            courses = db.Course.Where(x => x.CategoryId == id).OrderBy(x => x.Name).Skip((page - 1) * limiter).Take(limiter).ToList();
                        }
                        else if (orderBy == "desc")
                        {
                            courses = db.Course.Where(x => x.CategoryId == id).OrderByDescending(x => x.Name).Skip((page - 1) * limiter).Take(limiter).ToList();
                        }
                        break;
                    case "price":
                        if (orderBy == "asc")
                        {
                            courses = db.Course.Where(x => x.CategoryId == id).OrderBy(x => x.Price).Skip((page - 1) * limiter).Take(limiter).ToList();
                        }
                        else if (orderBy == "desc")
                        {
                            courses = db.Course.Where(x => x.CategoryId == id).OrderByDescending(x => x.Price).Skip((page - 1) * limiter).Take(limiter).ToList();
                        }
                        break;
                    default:
                        courses = db.Course.Where(x => x.CategoryId == id).Skip((page - 1) * limiter).Take(limiter).ToList();
                        break;
                }
            }

            return courses;
        }

        public ActionResult Course(int page = 1, string sorter = "name", string orderBy = "asc", int limiter = 12)
        {
            var id = RouteData.Values["id"];
            ViewBag.Sorter = sorter;
            ViewBag.OrderBy = orderBy;
            ViewBag.Limiter = limiter;
            if (id != null)
            {
                bool isInt = Int32.TryParse(id.ToString(), out int entityId);
                var categoryById = db.Category.FirstOrDefault(x => x.EntityId == entityId);
                if (categoryById != null)
                {
                    ViewBag.TotalPages = Math.Ceiling((decimal)db.Course.Where(x => x.CategoryId == entityId).AsEnumerable().Count() / limiter);
                    if (ViewBag.TotalPages == 1)
                    {
                        page = 1;
                    }
                    ViewBag.CurrentPage = page;
                    var childCategoriesById = db.Category.Where(x => x.ParentId == entityId).AsEnumerable();
                    var allChildCategoriesById = db.Category.Where(x => x.Path.Contains("/" + entityId + "/")).AsEnumerable();
                    var coursesByCatgoryId = this.GetCoursesByCondition(page, sorter, orderBy, limiter, entityId);
                    if (coursesByCatgoryId.Count() > 0)
                    {
                        ViewBag.Coures = coursesByCatgoryId;
                    }
                    else
                    {
                        ViewBag.Coures = null;
                    }
                    if (childCategoriesById.Count() > 0)
                    {
                        ViewBag.ChildCategories = childCategoriesById;
                    }
                    else
                    {
                        ViewBag.ChildCategories = null;
                    }
                    if (allChildCategoriesById.Count() > 0)
                    {
                        foreach (var item in allChildCategoriesById)
                        {
                            var coursesInChildCategory = db.Course.Where(x => x.CategoryId == item.EntityId).AsEnumerable();
                            if (coursesInChildCategory.Count() > 0)
                            {
                                foreach (var course in coursesInChildCategory)
                                {
                                    if (coursesByCatgoryId.FirstOrDefault(x => x.EntityId == course.EntityId) == null)
                                    {
                                        coursesByCatgoryId.Add(course);
                                    }
                                }
                            }
                        }
                        ViewBag.ChildCategories = childCategoriesById;
                        ViewBag.Coures = coursesByCatgoryId;
                    }
                }
            }
            else
            {
                ViewBag.TotalPages = Math.Ceiling((decimal)db.Course.AsEnumerable().Count() / limiter);
                if (ViewBag.TotalPages == 1)
                {
                    page = 1;
                }
                ViewBag.CurrentPage = page;
                ViewBag.Coures = this.GetCoursesByCondition(page, sorter, orderBy, limiter);
                ViewBag.ChildCategories = db.Category.Where(x => x.Level == 2).AsEnumerable();
            }
            return View();
        }

        [HttpGet]
        public ActionResult GetStartTime()
        {
            var StartTime = db.Admission.AsEnumerable();
            return Json(new
            {
                data = StartTime
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Faq()
        {
            return View();
        }
        public ActionResult ExamResult()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Application(RegisterInfo f)
        {
            f.CreatedAt = DateTime.Now;
            db.RegisterInfo.Add(f);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CourseDetail()
        {
            var id = RouteData.Values["id"];
            if (id != null)
            {
                bool isInt = Int32.TryParse(id.ToString(), out int entityId);
                var courseById = db.Course.FirstOrDefault(x => x.EntityId == entityId);
                if (courseById != null)
                {
                    return View(courseById);
                }
            }
            TempData["CoureNotExist"] = "This course is not exist!";
            return RedirectToAction("Course");
        }
    }
}
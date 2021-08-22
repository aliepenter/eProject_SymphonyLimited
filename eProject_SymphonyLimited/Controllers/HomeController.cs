using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            ViewBag.Adms = db.Admission.Join(db.Course,
               ad => ad.CourseId,
               co => co.EntityId,
               (ad, co) => new
               AdmissionViewModel
               {
                   EntityId = ad.EntityId,
                   Name = ad.Name,
                   Price = ad.Price,
                   StartTime = ad.StartTime,
                   EndTime = ad.EndTime,
                   QuantityStudent = ad.QuantityStudent,
                   Course = co.Image
               }).AsEnumerable();
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
                        switch (sorter)
                        {
                            case "name":
                                if (orderBy == "asc")
                                {
                                    coursesByCatgoryId = coursesByCatgoryId.OrderBy(x => x.Name).Skip((page - 1) * limiter).Take(limiter).ToList();
                                }
                                else if (orderBy == "desc")
                                {
                                    coursesByCatgoryId = coursesByCatgoryId.OrderByDescending(x => x.Name).Skip((page - 1) * limiter).Take(limiter).ToList();
                                }
                                break;
                            case "price":
                                if (orderBy == "asc")
                                {
                                    coursesByCatgoryId = coursesByCatgoryId.OrderBy(x => x.Price).Skip((page - 1) * limiter).Take(limiter).ToList();
                                }
                                else if (orderBy == "desc")
                                {
                                    coursesByCatgoryId = coursesByCatgoryId.OrderByDescending(x => x.Price).Skip((page - 1) * limiter).Take(limiter).ToList();
                                }
                                break;
                            default:
                                coursesByCatgoryId = coursesByCatgoryId.Skip((page - 1) * limiter).Take(limiter).ToList();
                                break;
                        }
                        ViewBag.TotalPages = Math.Ceiling((decimal)coursesByCatgoryId.Count() / limiter);
                        if (ViewBag.TotalPages == 1)
                        {
                            page = 1;
                        }
                        ViewBag.CurrentPage = page;
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
            var findUser = db.RegisterInfo.FirstOrDefault(x => x.Email == f.Email);
            if (findUser != null)
            {
                var senderEmail = new MailAddress("hoangcaolong2311@gmail.com", "Eternal Nightmare");
                var receiverEmail = new MailAddress(f.Email, "Receiver");
                var password = "Longdaica123";
                var smtp = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {

                    Subject = "Symphony Limited",
                    Body = "<span style='color:red'>helo</span>"
                })

                {
                    try
                    {
                        mess.IsBodyHtml = true;
                        smtp.Send(mess);
                        //TempData["SuccessMessage"] = "Send successful!";
                    }
                    catch (System.Exception)
                    {

                    }
                }
            }
            else
            {

            }
            return RedirectToAction("Index");
        }
        public ActionResult AdmissionDetail()
        {
            return View();
        }
        public ActionResult CourseDetail()
        {
            var id = RouteData.Values["id"];
            if (id != null)
            {
                bool isInt = Int32.TryParse(id.ToString(), out int entityId);
                var courseById = db.Course.FirstOrDefault(x => x.EntityId == entityId);
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
               }).FirstOrDefault(x => x.EntityId == entityId);
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
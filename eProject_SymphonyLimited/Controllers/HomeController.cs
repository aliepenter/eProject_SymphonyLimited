using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var adms = db.Admission.Join(db.Course,
               ad => ad.CourseId,
               co => co.EntityId,
               (ad, co) => new
               AdmsViewModel
               {
                   EntityId = ad.EntityId,
                   Name = ad.Name,
                   Price = ad.Price,
                   StartTime = ad.StartTime,
                   EndTime = ad.EndTime,
                   BillTime = ad.BillTime,
                   CourseId = ad.CourseId,
                   Course = co.Image
               }).Where(x => x.StartTime <= DateTime.Now && x.EndTime >= DateTime.Now).ToList();
            if (adms.Count() > 0)
            {
                ViewBag.admissionList = adms;
            }
            else
            {
                ViewBag.admissionList = null;
            }
            ViewBag.teacher = teacher;
            ViewBag.phoneInFooter = db.CoreConfigData.FirstOrDefault(x => x.Code == "phone_in_footer");
            ViewBag.emailInFooter = db.CoreConfigData.FirstOrDefault(x => x.Code == "email_in_footer");
            ViewBag.addressInFooter = db.CoreConfigData.FirstOrDefault(x => x.Code == "address_in_footer");
            ViewBag.partner = db.Partner.AsEnumerable();

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
            var adms = db.Admission.Join(db.Course,
               ad => ad.CourseId,
               co => co.EntityId,
               (ad, co) => new
               AdmsViewModel
               {
                   EntityId = ad.EntityId,
                   Name = ad.Name,
                   Price = ad.Price,
                   StartTime = ad.StartTime,
                   EndTime = ad.EndTime,
                   BillTime = ad.BillTime,
                   CourseId = ad.CourseId,
                   Course = co.Image
               }).Where(x => x.StartTime <= DateTime.Now && x.EndTime >= DateTime.Now).AsEnumerable();
            if (adms.Count() > 0)
            {
                ViewBag.Adms = adms;
            }
            else
            {
                ViewBag.Adms = null;
            }
            return View();

        }
        [HttpGet]
        public ActionResult GetDataEntranceExam(int page = 1, string key = null)
        {
            int pageSize = 6;
            List<AdmsViewModel> admissionModel = new List<AdmsViewModel>();

            //var admission = db.Admission.Join(db.Course,
            //                   ad => ad.CourseId,
            //                   co => co.EntityId,
            //                   (ad, co) => new
            //                   AdmsViewModel
            //                   {
            //                       EntityId = ad.EntityId,
            //                       Name = ad.Name,
            //                       Price = ad.Price,
            //                       StartTime = ad.StartTime,
            //                       EndTime = ad.EndTime,
            //                       BillTime = ad.BillTime,
            //                       CourseId = ad.CourseId,
            //                       Course = co.Image
            //                   }).Where(x => x.StartTime <= DateTime.Now && x.EndTime >= DateTime.Now).AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                foreach (var item in db.Admission.Include(x => x.Course).Where(x => x.Name.ToString().Contains(key)))
                {
                    admissionModel.Add(new AdmsViewModel
                    {
                        EntityId = item.EntityId,
                        Name = item.Name,
                        Price = item.Price,
                        StartTime = item.StartTime,
                        StartTimeToDate = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                        EndTime = item.EndTime,
                        EndTimeToDate = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                        BillTime = item.BillTime,
                        BillTimeToDate = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                        PassedMark = item.PassedMark,
                        MaxMark = item.MaxMark,
                        Course = item.Course.Image
                    });
                }
            }
            else
            {
                foreach (var item in db.Admission.Include(x => x.Course))
                {
                    admissionModel.Add(new AdmsViewModel
                    {
                        EntityId = item.EntityId,
                        Name = item.Name,
                        Price = item.Price,
                        StartTime = item.StartTime,
                        StartTimeToDate = String.Format("{0:dd/MM/yyyy}", item.StartTime),
                        EndTime = item.EndTime,
                        EndTimeToDate = String.Format("{0:dd/MM/yyyy}", item.EndTime),
                        BillTime = item.BillTime,
                        BillTimeToDate = String.Format("{0:dd/MM/yyyy}", item.BillTime),
                        PassedMark = item.PassedMark,
                        MaxMark = item.MaxMark,
                        Course = item.Course.Image
                    });
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
                    if (categoryById.ParentId == 1)
                    {
                        ViewBag.ParentHref = "";
                    }
                    else
                    {
                        ViewBag.ParentHref = categoryById.ParentId;
                    }
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
                var emailAdms = db.Admission.FirstOrDefault(x => x.EntityId == f.AdmissionId);
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {

                    Subject = "Symphony Limited",
                    Body = "<div style=\"background: #fff; margin: 0 auto; width: 300px;    width: 500px; " +
                    "padding: 20px; border: 1px solid red;\">" +
                                "<div  style= \" text-align: left\">" +
                                    "<h1>Welcome " + f.Name + ",</h1>" +
                                    "<span>You have just registered for <span style=\"font-weight: bold; font-size: 16px\">" + emailAdms.Name + "</span>. <br/>" +
                                    "<span>The entrance examination fees is <span style=\"font-weight: bold; font-size: 16px\">" + emailAdms.Price + " $</span></span><br/>" +
                                    "Please go to the nearest Symphony Limited center to process the " +
                                    "payment before <span style=\"font-weight: bold; font-size: 16px\">" + emailAdms.EndTime + "</span> to participate in the entrance exam.<br/></ span>" +
                                    "<span>Thank you,<br/></span>" +
                                    "<span style = \"font-weight: bold\">Symphony Limited</span>" +
                                "</div>" +
                            "</div>"
                })

                {
                    try
                    {
                        mess.IsBodyHtml = true;
                        smtp.Send(mess);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            else
            {

            }
            return RedirectToAction("Index");
        }
        public ActionResult Faq()
        {
            ViewBag.faq = db.Faq.AsEnumerable();
            var id = RouteData.Values["id"];
            if (id != null)
            {
                bool isInt = Int32.TryParse(id.ToString(), out int entityId);
                var ans = db.Admission.FirstOrDefault(x => x.EntityId == entityId);
                ViewBag.ans = db.Faq.FirstOrDefault(x => x.EntityId == entityId);
            }
            return View();
        }

        public ActionResult AdmissionDetail()
        {
            var id = RouteData.Values["id"];
            if (id != null)
            {
                bool isInt = Int32.TryParse(id.ToString(), out int entityId);
                var admById = db.Admission.FirstOrDefault(x => x.EntityId == entityId);
                ViewBag.admById = db.Admission.Join(db.Course,
               ad => ad.CourseId,
               co => co.EntityId,
               (ad, co) => new
               AdmsViewModel
               {
                   EntityId = ad.EntityId,
                   Name = ad.Name,
                   Price = ad.Price,
                   StartTime = ad.StartTime,
                   EndTime = ad.EndTime,
                   BillTime = ad.BillTime,
                   MaxMark = ad.MaxMark,
                   PassedMark = ad.PassedMark,
                   CourseId = ad.CourseId,
                   Image = co.Image,
                   Course = co.Name
               }).FirstOrDefault(x => x.EntityId == entityId);
            }
            var adms = db.Admission.Join(db.Course,
               ad => ad.CourseId,
               co => co.EntityId,
               (ad, co) => new
               AdmsViewModel
               {
                   EntityId = ad.EntityId,
                   Name = ad.Name,
                   Price = ad.Price,
                   StartTime = ad.StartTime,
                   EndTime = ad.EndTime,
                   BillTime = ad.BillTime,
                   CourseId = ad.CourseId,
                   Course = co.Image
               }).Where(x => x.StartTime <= DateTime.Now && x.EndTime >= DateTime.Now).AsEnumerable();
            if (adms.Count() > 0)
            {
                ViewBag.Adms = adms;
            }
            else
            {
                ViewBag.Adms = null;
            }
            return View();
        }
        public ActionResult CourseDetail()
        {
            var id = RouteData.Values["id"];
            if (id != null)
            {
                bool isInt = Int32.TryParse(id.ToString(), out int entityId);
                var courseById = db.Course.FirstOrDefault(x => x.EntityId == entityId);
                var admission = db.Admission.FirstOrDefault(x => x.CourseId == entityId && x.StartTime < DateTime.Now && x.EndTime > DateTime.Now);
                if (admission != null)
                {
                    ViewBag.HasAdmission = true;
                    ViewBag.AdmissionId = admission.EntityId;
                }
                else
                {
                    ViewBag.HasAdmission = false;
                }
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

        [HttpGet]
        public ActionResult GetOldAdmission(string key = null)
        {
            var oldAdmissions = db.Admission.Where(x => x.EndTime < DateTime.Now).AsEnumerable();
            string jsonData = "[]";
            if (!String.IsNullOrEmpty(key))
            {
                oldAdmissions = db.Admission.Where(x => x.EndTime < DateTime.Now && x.Name.Contains(key)).AsEnumerable();
            }
            if (oldAdmissions.Count() > 0)
            {
                jsonData = JsonConvert.SerializeObject(oldAdmissions);
                return Json(new
                {
                    StatusCode = 200,
                    Data = jsonData
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                StatusCode = 400,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetResultByOldAdmission(int admissionId)
        {
            string jsonData = "[]";
            var result = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg,
                           ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               {
                   re,
                   pr
               }).Join(db.Course,
               ad => ad.re.ad.CourseId,
               co => co.EntityId,
               (ad, co) => new
               PaidRegisterViewModel
               {
                   EntityId = ad.pr.EntityId,
                   RollNumber = ad.pr.RollNumber,
                   Result = ad.pr.Result,
                   Name = ad.re.reg.Name,
                   Phone = ad.re.reg.Phone,
                   Email = ad.re.reg.Email,
                   Comment = ad.re.reg.Comment,
                   CreatedAt = ad.re.reg.CreatedAt,
                   AdmissionId = ad.re.reg.AdmissionId,
                   Admission = ad.re.ad.Name,
                   Tested = ad.pr.Tested,
                   BillTime = ad.re.ad.BillTime,
                   CourseFee = co.Price
               }).Where(x => x.AdmissionId == admissionId && x.Tested == true);
            if (result.Count() > 0)
            {
                jsonData = JsonConvert.SerializeObject(result);
                return Json(new
                {
                    StatusCode = 200,
                    Data = jsonData
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                StatusCode = 400,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ExamResult(string rollNumber)
        {
            var resultByRollNumber = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg,
                           ad
                       })
                .Join(
                    db.PaidRegister,
                       re => re.reg.EntityId,
                       pr => pr.RegisterInfoId,
                       (re, pr) => new
                       {
                           re,
                           pr
                       }).Join(
                db.Course,
                ad => ad.re.ad.CourseId,
                co => co.EntityId,
                (ad, co) => new
                PaidRegisterViewModel
                {
                    EntityId = ad.pr.EntityId,
                    RollNumber = ad.pr.RollNumber,
                    Result = ad.pr.Result,
                    Name = ad.re.reg.Name,
                    Phone = ad.re.reg.Phone,
                    Email = ad.re.reg.Email,
                    Comment = ad.re.reg.Comment,
                    CreatedAt = ad.re.reg.CreatedAt,
                    AdmissionId = ad.re.reg.AdmissionId,
                    Admission = ad.re.ad.Name,
                    Tested = ad.pr.Tested,
                    BillTime = ad.re.ad.BillTime,
                    CourseFee = co.Price
                }).FirstOrDefault(x => x.RollNumber == rollNumber);
            if (resultByRollNumber != null)
            {
                if (resultByRollNumber.Tested == false)
                {
                    TempData["NotTested"] = "Your entrance exam is not start!";
                    ViewBag.Result = null;
                }
                else
                {
                    ViewBag.Result = resultByRollNumber;
                }
            }
            else
            {
                TempData["RollNumberNotExist"] = "Your roll number is not exist!";
                ViewBag.Result = null;
            }
            return View();
        }
    }
}
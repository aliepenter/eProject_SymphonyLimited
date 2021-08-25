using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class RegisterInfoController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 10;
            var registerInfos = db.RegisterInfo.Join(db.Admission,
               re => re.AdmissionId,
               ad => ad.EntityId,
               (re, ad) => new
               RegisterViewModel
               {
                   EntityId = re.EntityId,
                   Name = re.Name,
                   Phone = re.Phone,
                   Email = re.Email,
                   Comment = re.Comment,
                   CreatedAt = re.CreatedAt,
                   Status = re.Status,
                   Admission = ad.Name
               }).AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
                        registerInfos = db.RegisterInfo.Join(db.Admission,
                           re => re.AdmissionId,
                           ad => ad.EntityId,
                           (re, ad) => new
                           RegisterViewModel
                           {
                               EntityId = re.EntityId,
                               Name = re.Name,
                               Phone = re.Phone,
                               Email = re.Email,
                               Comment = re.Comment,
                               CreatedAt = re.CreatedAt,
                               Status = re.Status,
                               Admission = ad.Name
                           }).Where(x => x.EntityId.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Name":
                        registerInfos = db.RegisterInfo.Join(db.Admission,
                       re => re.AdmissionId,
                       ad => ad.EntityId,
                       (re, ad) => new
                       RegisterViewModel
                       {
                           EntityId = re.EntityId,
                           Name = re.Name,
                           Phone = re.Phone,
                           Email = re.Email,
                           Comment = re.Comment,
                           CreatedAt = re.CreatedAt,
                           Status = re.Status,
                           Admission = ad.Name
                       }).Where(x => x.Name.Contains(key)).AsEnumerable();
                        break;
                    case "Phone":
                        registerInfos = db.RegisterInfo.Join(db.Admission,
                       re => re.AdmissionId,
                       ad => ad.EntityId,
                       (re, ad) => new
                       RegisterViewModel
                       {
                           EntityId = re.EntityId,
                           Name = re.Name,
                           Phone = re.Phone,
                           Email = re.Email,
                           Comment = re.Comment,
                           CreatedAt = re.CreatedAt,
                           Status = re.Status,
                           Admission = ad.Name
                       }).Where(x => x.Phone.Contains(key)).AsEnumerable();
                        break;
                    case "Email":
                        registerInfos = db.RegisterInfo.Join(db.Admission,
                       re => re.AdmissionId,
                       ad => ad.EntityId,
                       (re, ad) => new
                       RegisterViewModel
                       {
                           EntityId = re.EntityId,
                           Name = re.Name,
                           Phone = re.Phone,
                           Email = re.Email,
                           Comment = re.Comment,
                           CreatedAt = re.CreatedAt,
                           Status = re.Status,
                           Admission = ad.Name
                       }).Where(x => x.Email.Contains(key)).AsEnumerable();
                        break;
                    case "Status":
                        registerInfos = db.RegisterInfo.Join(db.Admission,
                       re => re.AdmissionId,
                       ad => ad.EntityId,
                       (re, ad) => new
                       RegisterViewModel
                       {
                           EntityId = re.EntityId,
                           Name = re.Name,
                           Phone = re.Phone,
                           Email = re.Email,
                           Comment = re.Comment,
                           CreatedAt = re.CreatedAt,
                           Status = re.Status,
                           Admission = ad.Name
                       }).Where(x => x.Status.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Admission":
                        registerInfos = db.RegisterInfo.Join(db.Admission,
                       re => re.AdmissionId,
                       ad => ad.EntityId,
                       (re, ad) => new
                       RegisterViewModel
                       {
                           EntityId = re.EntityId,
                           Name = re.Name,
                           Phone = re.Phone,
                           Email = re.Email,
                           Comment = re.Comment,
                           CreatedAt = re.CreatedAt,
                           Status = re.Status,
                           Admission = ad.Name
                       }).Where(x => x.Admission.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)registerInfos.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(registerInfos.Skip((page - 1) * pageSize).Take(pageSize));
            return Json(new
            {
                TotalPages = totalPages,
                CurrentPage = page,
                StatusCode = 200,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int id)
        {

            var reById = db.RegisterInfo.FirstOrDefault(x => x.EntityId == id);

            if (reById != null)
            {
                return View(reById);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(RegisterInfo r)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var adId = db.RegisterInfo.FirstOrDefault(x => x.EntityId == r.EntityId);
                    var e = db.Admission.FirstOrDefault(x => x.EntityId == adId.AdmissionId);
                    var reById = db.RegisterInfo.FirstOrDefault(x => x.EntityId == r.EntityId);
                    if (reById != null)
                    {
                        if (reById.Status == true && r.Status == false)
                        {
                            TempData["ErrorMess"] = "Can't edit status!";
                        }
                        else
                        {
                            reById.Name = r.Name;
                            reById.Phone = r.Phone;
                            reById.Email = r.Email;
                            reById.Status = r.Status;

                            if (db.PaidRegister.FirstOrDefault(x => x.RegisterInfoId == r.EntityId) == null)
                            {
                                PaidRegister addPaidRegister = new PaidRegister();
                                addPaidRegister.RollNumber = "SL0" + r.EntityId;
                                addPaidRegister.RegisterInfoId = r.EntityId;
                                db.PaidRegister.Add(addPaidRegister);
                                db.SaveChanges();
                                var senderEmail = new MailAddress("hoangcaolong2311@gmail.com", "Eternal Nightmare");
                                var receiverEmail = new MailAddress(r.Email, "Receiver");
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
                                    Body = "<div style=\"background: #fff; margin: 0 auto; width: 300px;    width: 500px; " +
                                    "padding: 20px; border: 1px solid red;\">" +
                                                "<div  style= \" text-align: left\">" +
                                                    "<h1>Dear: " + r.Name + "</h1>" +
                                                    "<span>You have paid for <span style=\"font-weight: bole; font-size: 20px\">" + e.Name + "</span>'s Entrance Examination. " +
                                                    "Your roll number is <span style=\"font-weight: bole; font-size: 20px\">" + addPaidRegister.RollNumber + "</span>.</ span>" +
                                                    "The exam will start on <span style=\"font-weight: bole; font-size: 20px\">" + e.EndTime + "</span>.<br/></ span>" +
                                                    "<span>Good luck with your exam!<br/></span>" +
                                                    "<span>Thank you,<br/></span>" +
                                                    "<span style = \"font-weight: bold\">Symphony Limited</span>" +
                                                "</div>" +
                                            "</div>"
                                })
                                    try
                                    {
                                        mess.IsBodyHtml = true;
                                        smtp.Send(mess);
                                    }
                                    catch (Exception)
                                    {

                                    }
                            }
                            
                            return RedirectToAction("Index");
                        }
                    }

                }
                catch (Exception)
                {

                }
            }
            ViewBag.Admissions = db.Admission.ToList();
            return View();
        }
        public ActionResult Delete(int id)
        {
            if (db.RegisterInfo.Find(id) != null)
            {
                try
                {
                    db.RegisterInfo.Remove(db.RegisterInfo.Find(id));
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
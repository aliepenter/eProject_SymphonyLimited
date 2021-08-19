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
    public class RegisterInfoController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/RegisterInfo
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
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
                       Admission = ad.Name }).Where(x => x.EntityId.ToString().Contains(key)).AsEnumerable();
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
                ViewBag.Admissions = db.Admission.ToList();
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
                    
                    var reById = db.RegisterInfo.FirstOrDefault(x => x.EntityId == r.EntityId);

                    if (reById != null)
                    {
                        if (reById.Status == true && r.Status == false)
                        {
                            
                            TempData["ErrorMess"] = "Can't edit status!";
                        }
                        else
                        {
                            reById.EntityId = r.EntityId;
                            reById.Name = r.Name;
                            reById.Phone = r.Phone;
                            reById.Email = r.Email;
                            reById.Comment = r.Comment;
                            reById.CreatedAt = r.CreatedAt;
                            reById.AdmissionId = r.AdmissionId;
                            reById.Status = true;
                            db.SaveChanges();

                            var paidRegister = db.RegisterInfo.Where(c => c.Status != false);

                            foreach (var item in paidRegister)
                            {
                                var obj = new PaidRegister()
                                {
                                    RollNumber = "SL0" + item.EntityId,
                                    RegisterInfoId = item.EntityId
                                };
                                db.PaidRegister.Add(obj);
                            }
                            db.SaveChanges();

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
using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
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
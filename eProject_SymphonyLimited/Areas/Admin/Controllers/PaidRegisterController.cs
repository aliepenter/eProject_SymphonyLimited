using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class PaidRegisterController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/PaidRegister
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null, int admissionId = 0)
        {
            int pageSize = 5;
            var PaidRegisters = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg, ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name
               }).Where(x => x.AdmissionId == admissionId).AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
                        PaidRegisters = db.RegisterInfo
                .Join(
                    db.Admission,
                       reg => reg.AdmissionId,
                       ad => ad.EntityId,
                       (reg, ad) => new
                       {
                           reg, ad
                       })
                .Join(
            db.PaidRegister,
               re => re.reg.EntityId,
               pr => pr.RegisterInfoId,
               (re, pr) => new
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name
               }).Where(x => x.EntityId.ToString().Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "Name":
                        PaidRegisters = db.RegisterInfo
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
              PaidRegisterViewModel
              {
                  EntityId = pr.EntityId,
                  RollNumber = pr.RollNumber,
                  Result = pr.Result,
                  Name = re.reg.Name,
                  Phone = re.reg.Phone,
                  Email = re.reg.Email,
                  Comment = re.reg.Comment,
                  CreatedAt = re.reg.CreatedAt,
                  AdmissionId = re.reg.AdmissionId,
                  Admission = re.ad.Name
              }).Where(x => x.Name.Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "Phone":
                        PaidRegisters = db.RegisterInfo
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
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name
               }).Where(x => x.Phone.Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "Email":
                        PaidRegisters = db.RegisterInfo
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
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name
               }).Where(x => x.Email.Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "Status":
                        PaidRegisters = db.RegisterInfo
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
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name
               }).Where(x => x.Status.ToString().Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    case "Admission":
                        PaidRegisters = db.RegisterInfo
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
               PaidRegisterViewModel
               {
                   EntityId = pr.EntityId,
                   RollNumber = pr.RollNumber,
                   Result = pr.Result,
                   Name = re.reg.Name,
                   Phone = re.reg.Phone,
                   Email = re.reg.Email,
                   Comment = re.reg.Comment,
                   CreatedAt = re.reg.CreatedAt,
                   AdmissionId = re.reg.AdmissionId,
                   Admission = re.ad.Name
               }).Where(x => x.Admission.Contains(key) && x.AdmissionId == admissionId).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)PaidRegisters.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(PaidRegisters.Skip((page - 1) * pageSize).Take(pageSize));
            return Json(new
            {
                TotalPages = totalPages,
                CurrentPage = page,
                StatusCode = 200,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

    }
}
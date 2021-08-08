using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class AttributeController : Controller
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Attribute
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAttributes(int page = 1, string key = null)
        {
            int pageSize = 5;
            var attributes = db.EavAttribute.Join(db.EavEntity,
                a => a.EavEntityId,
                e => e.EntityId,
                (a, e) => new
                {
                    EntityId = a.EntityId,
                    Name = a.Name,
                    AttributeCode = a.Code,
                    Entity = e.Name
                }).OrderBy(x => x.EntityId).AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                attributes = db.EavAttribute.Join(db.EavEntity,
                a => a.EavEntityId,
                e => e.EntityId,
                (a, e) => new
                {
                    EntityId = a.EntityId,
                    Name = a.Name,
                    AttributeCode = a.Code,
                    Entity = e.Name
                }).OrderBy(x => x.EntityId).Where(x => x.Name.Contains(key)).AsEnumerable();
            }
            decimal totalPages = Math.Ceiling((decimal)attributes.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(attributes.Skip((page - 1) * pageSize).Take(pageSize));
            return Json(new
            {
                TotalPages = totalPages,
                CurrentPage = page,
                StatusCode = 200,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostEntity(EavEntity e)
        {
            if (ModelState.IsValid)
            {
                var entity = new EavEntity
                {
                    Name = e.Name,
                    Code = e.Code
                };
                db.EavEntity.Add(entity);
                db.SaveChanges();
                return Json(new
                {
                    StatusCode = 200,
                    Data = entity,
                    Message = "Add entity success!"
                }, JsonRequestBehavior.AllowGet);
            }
            Dictionary<string, string> errors = new Dictionary<string, string>();
            foreach (var k in ModelState.Keys)
            {
                foreach (var err in ModelState[k].Errors)
                {
                    string key = Regex.Replace(k, @"(\w+)\.(\w+)", @"$2");
                    if (!errors.ContainsKey(key))
                    {
                        errors.Add(key, err.ErrorMessage);
                    }
                }
            }
            return Json(new
            {
                StatusCode = 400,
                Data = errors,
                Message = "Add entity fail!"
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
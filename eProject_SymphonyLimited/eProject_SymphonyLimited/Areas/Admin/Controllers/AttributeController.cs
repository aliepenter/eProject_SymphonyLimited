using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                    Code = e.Name.ToLower()
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
                        errors.Add("EavEntity" + key, err.ErrorMessage);
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

        [HttpGet]
        public ActionResult GetEntitties()
        {
            var entities = db.EavEntity.AsEnumerable();
            string jsonData = JsonConvert.SerializeObject(entities);
            return Json(new
            {
                StatusCode = 200,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAttributeById(int id)
        {
            var attributeById = db.EavAttribute.Find(id);
            string jsonData = JsonConvert.SerializeObject(attributeById);
            return Json(new
            {
                StatusCode = 200,
                Data = jsonData
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostAttribute(EavAttribute e)
        {
            if (ModelState.IsValid)
            {
                var attribute = new EavAttribute
                {
                    Name = e.Name,
                    Code = e.Name.ToLower(),
                    EavEntityId = e.EavEntityId
                };
                db.EavAttribute.Add(attribute);
                db.SaveChanges();
                return Json(new
                {
                    StatusCode = 200,
                    Data = attribute,
                    Message = "Add attribute success!"
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
                        errors.Add("EavAttribute" + key, err.ErrorMessage);
                    }
                }
            }
            return Json(new
            {
                StatusCode = 400,
                Data = errors,
                Message = "Add attribute fail!"
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateAttribute(EavAttribute e)
        {
            var attributeById = db.EavAttribute.Find(e.EntityId);
            if (attributeById != null)
            {
                if (ModelState.IsValid)
                {
                    attributeById.Code = e.Name.ToLower();
                    attributeById.Name = e.Name;
                    attributeById.EavEntityId = e.EavEntityId;
                    db.SaveChanges();
                    return Json(new
                    {
                        StatusCode = 200,
                        Data = e,
                        Message = "Update attribute success!"
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
                            errors.Add("EavAttribute" + key, err.ErrorMessage);
                        }
                    }
                }
                return Json(new
                {
                    StatusCode = 400,
                    Data = errors,
                    Message = "Update attribute fail!"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    StatusCode = 400,
                    Message = "Can't find attribute with id = " + e.EntityId + "!"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteAttributeById(int id)
        {
            var attributeById = db.EavAttribute.Find(id);
            if (attributeById != null)
            {
                db.EavAttribute.Remove(attributeById);
                db.SaveChanges();
                return Json(new
                {
                    StatusCode = 200,
                    Message = "Delete attribute with id = " + id + " success!"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    StatusCode = 400,
                    Message = "Can't find attribute with id = " + id + "!"
                }, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class PartnerController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Partner
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
            var partners = db.Partner.AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
                        partners = db.Partner.Where(x => x.EntityId.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Name":
                        partners = db.Partner.Where(x => x.Name.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)partners.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(partners.Skip((page - 1) * pageSize).Take(pageSize));
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
            return View();
        }

        [HttpPost]
        public ActionResult Create(Partner p, HttpPostedFileBase imgFile)
        {
            var validateName = db.Partner.FirstOrDefault(x => x.Name == p.Name);
            if (validateName != null)
            {
                ModelState.AddModelError("Name", "Partner name can't be the same!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (imgFile == null && p.Image == null)
                    {
                        p.Image = "default.png";
                        db.Partner.Add(p);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string imgName = Path.GetFileName(imgFile.FileName);
                        string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                        p.Image = imgName;
                        if (imgFile.ContentLength > 0)
                        {
                            db.Partner.Add(p);
                            if (db.SaveChanges() > 0)
                            {
                                imgFile.SaveAs(imgPath);
                                ModelState.Clear();
                            }
                        }
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save partner!");
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var partnerById = db.Partner.FirstOrDefault(x => x.EntityId == id);
            if (partnerById != null)
            {
                return View(partnerById);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Partner p, HttpPostedFileBase imgFile)
        {
            var currentPartner = db.Partner.Find(p.EntityId);
            var validateName = db.Partner.FirstOrDefault(x => x.Name != currentPartner.Name && x.Name == p.Name);
            if (validateName != null)
            {
                ModelState.AddModelError("Name", "Partner name can't be the same!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    currentPartner.Name = p.Name;
                    if (imgFile != null)
                    {
                        string imgName = Path.GetFileName(imgFile.FileName);
                        string imgText = Path.GetExtension(imgName);
                        string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                        currentPartner.Image = imgName;
                        if (imgFile.ContentLength > 0)
                        {
                            if (db.SaveChanges() > 0)
                            {
                                imgFile.SaveAs(imgPath);
                            }
                        }
                    }
                    else
                    {
                        db.SaveChanges();
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save partner!");
                }
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (db.Partner.Find(id) != null)
            {
                try
                {
                    db.Partner.Remove(db.Partner.Find(id));
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
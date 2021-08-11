using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.Linq;
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
        public ActionResult Get(int page = 1, string key = null)
        {
            int pageSize = 5;
            var partners = db.Partner.AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                partners = db.Partner.Where(x => x.Name.Contains(key)).AsEnumerable();
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
        public ActionResult Create(Partner p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Partner.Add(p);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

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
        public ActionResult Edit(Partner p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {

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
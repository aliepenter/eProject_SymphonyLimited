using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            return View(db.Partner.AsEnumerable());
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
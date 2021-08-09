using eProject_SymphonyLimited.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class FaqController : Controller
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Faq
        public ActionResult Index()
        {
            return View(db.Faq.AsEnumerable());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Faq f)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Faq.Add(f);
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
            var faqById = db.Faq.FirstOrDefault(x => x.EntityId == id);
            if (faqById != null)
            {
                return View(faqById);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Faq f)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(f).State = EntityState.Modified;
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
            if (db.Faq.Find(id) != null)
            {
                try
                {
                    db.Faq.Remove(db.Faq.Find(id));
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
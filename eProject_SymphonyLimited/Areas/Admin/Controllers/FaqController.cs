using eProject_SymphonyLimited.Models;
using System;
using Newtonsoft.Json;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using eProject_SymphonyLimited.Areas.Admin.Data;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class FaqController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/Faq
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
            var faqs = db.Faq.AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
                        faqs = db.Faq.Where(x => x.EntityId.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Question":
                        faqs = db.Faq.Where(x => x.Question.Contains(key)).AsEnumerable();
                        break;
                    case "Answer":
                        faqs = db.Faq.Where(x => x.Answer.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)faqs.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(faqs.Skip((page - 1) * pageSize).Take(pageSize));
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
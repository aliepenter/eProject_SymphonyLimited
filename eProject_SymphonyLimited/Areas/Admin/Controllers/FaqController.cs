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
            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
            {
                switch (type)
                {
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
                var validateQuestion = db.Faq.FirstOrDefault(x => x.Question == f.Question);
                var validateAnswer = db.Faq.FirstOrDefault(x => x.Answer == f.Answer);
                if (validateQuestion == null)
                {
                    if (validateAnswer == null)
                    {
                        try
                        {
                            db.Faq.Add(f);
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError("", "Some thing went wrong while save faq!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Answer", "Faq Answer is already exist!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Question", "Faq Question is already exist!");
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
                var currentFaq = db.Faq.Find(f.EntityId);
                var validateQuestion = db.Faq.FirstOrDefault(x => x.Question != currentFaq.Question && x.Question == f.Question);
                var validateAnswer = db.Faq.FirstOrDefault(x => x.Answer != currentFaq.Answer && x.Answer == f.Answer);
                if (validateQuestion == null)
                {
                    if (validateAnswer == null)
                    {
                        try
                        {
                            currentFaq.Question = f.Question;
                            currentFaq.Answer = f.Answer;
                            db.SaveChanges();
                            return RedirectToAction("Index");
                        }
                        catch (Exception)
                        {
                            ModelState.AddModelError("", "Some thing went wrong while save faq!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Answer", "Faq Answer is already exist!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Question", "Faq Question is already exist!");
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
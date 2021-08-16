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
    public class TeacherController : BaseController
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
            var teachers = db.Teacher.AsEnumerable();
            if (!String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "EntityId":
                        teachers = db.Teacher.Where(x => x.EntityId.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "Name":
                        teachers = db.Teacher.Where(x => x.Name.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)teachers.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(teachers.Skip((page - 1) * pageSize).Take(pageSize));
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
        public ActionResult Create(Teacher t, HttpPostedFileBase imgFile)
        {
            var validateName = db.Teacher.FirstOrDefault(x => x.Name == t.Name);
            if (validateName != null)
            {
                ModelState.AddModelError("Name", "Teacher name can't be the same!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (imgFile == null && t.Image == null)
                    {
                        t.Image = "default.png";
                        db.Teacher.Add(t);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        string imgName = Path.GetFileName(imgFile.FileName);
                        string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                        t.Image = imgName;
                        if (imgFile.ContentLength > 0)
                        {
                            db.Teacher.Add(t);
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
                    ModelState.AddModelError("", "Some thing went wrong while save teacher!");
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var teacherId = db.Teacher.Find(id);
            if (teacherId != null)
            {
                return View(teacherId);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Teacher t, HttpPostedFileBase imgFile)
        {
            var currentTeacher = db.Teacher.Find(t.EntityId);
            var validateName = db.Teacher.FirstOrDefault(x => x.Name != currentTeacher.Name && x.Name == t.Name);
            if (validateName != null)
            {
                ModelState.AddModelError("Name", "Teacher name can't be the same!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    currentTeacher.Name = t.Name;
                    currentTeacher.Specialize = t.Specialize;
                    currentTeacher.Subject = t.Subject;
                    if (imgFile != null)
                    {
                        string imgName = Path.GetFileName(imgFile.FileName);
                        string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                        currentTeacher.Image = imgName;
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
            if (db.Teacher.Find(id) != null)
            {
                try
                {
                    db.Teacher.Remove(db.Teacher.Find(id));
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
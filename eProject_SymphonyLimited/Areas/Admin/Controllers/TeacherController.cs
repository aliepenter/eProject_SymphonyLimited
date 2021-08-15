using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Models;
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
        // GET: Admin/Lecturer
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();
        // GET: Admin/Lecturer
        public ActionResult Index()
        {
            ViewBag.Lecturers = db.Teacher.AsEnumerable();

            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Teacher t, HttpPostedFileBase lecturerimg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string lecturerImgName = Path.GetFileName(lecturerimg.FileName);
                    string lecturerImgText = Path.GetExtension(lecturerImgName);
                    string lecturerImgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/Teacher/"), lecturerImgName);
                    t.Image = "~/Areas/Admin/Content/img/Teacher/" + lecturerImgName;
                    if (lecturerimg.ContentLength > 0)
                    {
                        db.Teacher.Add(t);
                        if (db.SaveChanges() > 0)
                        {
                            lecturerimg.SaveAs(lecturerImgPath);
                            ViewBag.msg = "Record Added";
                            ModelState.Clear();
                        }
                    }
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
            var Lecturer = db.Teacher.FirstOrDefault(x => x.EntityId == id);
            if (Lecturer != null)
            {
                Session["lecturerImgPath"] = Lecturer.Image;
                return View(Lecturer);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Teacher t, HttpPostedFileBase lecturerimg)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (lecturerimg != null)
                    {
                        string lecturerImgName = Path.GetFileName(lecturerimg.FileName);
                        string lecturerImgText = Path.GetExtension(lecturerImgName);
                        string lecturerImgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/Teacher/"), lecturerImgName);
                        t.Image = "~/Areas/Admin/Content/img/Teacher/" + lecturerImgName;
                        if (lecturerimg.ContentLength > 0)
                        {
                            db.Entry(t).State = EntityState.Modified;
                            string oldImagePath = Request.MapPath(Session["lecturerImgPath"].ToString());
                            if (db.SaveChanges() > 0)
                            {
                                lecturerimg.SaveAs(lecturerImgPath);
                                TempData["msg"] = "Record Updated";
                                //ViewBag.msg = "Record Added";
                                //ModelState.Clear();
                            }
                        }

                    }
                    else
                    {
                        t.Image = Session["lecturerImgPath"].ToString();
                        db.Entry(t).State = EntityState.Modified;
                        if (db.SaveChanges() > 0)
                        {
                            TempData["msg"] = "Data Updated";
                        }
                    }
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
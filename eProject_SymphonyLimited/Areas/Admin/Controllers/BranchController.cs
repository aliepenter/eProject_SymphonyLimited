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
    public class BranchController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/CoreConfigData
        public ActionResult Index()
        {
            return View(db.Branch.AsEnumerable());
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
            var branchs = db.Branch.AsEnumerable();
            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "Name":
                        branchs = db.Branch.Where(x => x.Name.Contains(key)).AsEnumerable();
                        break;
                    case "Email":
                        branchs = db.Branch.Where(x => x.Email.Contains(key)).AsEnumerable();
                        break;
                    case "Address":
                        branchs = db.Branch.Where(x => x.Address.Contains(key)).AsEnumerable();
                        break; 
                    case "Phone":
                        branchs = db.Branch.Where(x => x.Phone.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)branchs.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(branchs.Skip((page - 1) * pageSize).Take(pageSize));
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
        public ActionResult Create(Branch b, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                var validateName = db.Branch.FirstOrDefault(x => x.Name == b.Name);
                var validateAddress = db.Branch.FirstOrDefault(x => x.Address == b.Address);
                if (validateName == null)
                {
                    if (validateAddress == null)
                    {
                        try
                        {
                            if (imgFile == null && b.Image == null)
                            {
                                b.Image = "default.png";
                                db.Branch.Add(b);
                                db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                string imgName = Path.GetFileName(imgFile.FileName);
                                string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                                b.Image = imgName;
                                if (imgFile.ContentLength > 0)
                                {
                                    db.Branch.Add(b);
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
                            ModelState.AddModelError("", "Some thing went wrong while save branch!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Address", "Branch Address is already exist!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "Branch Name is already exist!");
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var branch = db.Branch.FirstOrDefault(x => x.EntityId == id);
            if (branch != null)
            {
                return View(branch);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Branch b, HttpPostedFileBase imgFile)
        {
            if (ModelState.IsValid)
            {
                var currentBranch = db.Branch.Find(b.EntityId);
                var validateName = db.Branch.FirstOrDefault(x => x.Name != currentBranch.Name && x.Name == b.Name);
                var validateAddress = db.Branch.FirstOrDefault(x => x.Address != currentBranch.Address && x.Address == b.Address);
                if (validateName == null)
                {
                    if (validateAddress == null)
                    {
                        try
                        {
                            currentBranch.Name = b.Name;
                            currentBranch.Email = b.Email;
                            currentBranch.Time = b.Time;
                            currentBranch.Phone = b.Phone;
                            currentBranch.Address = b.Address;
                            currentBranch.Description = b.Description;
                            if (imgFile != null)
                            {
                                string imgName = Path.GetFileName(imgFile.FileName);
                                string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                                currentBranch.Image = imgName;
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
                            ModelState.AddModelError("", "Some thing went wrong while save branch!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Address", "Branch Address is already exist!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Name", "Branch Name is already exist!");
                }

            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            if (db.Branch.Find(id) != null)
            {
                try
                {
                    db.Branch.Remove(db.Branch.Find(id));
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while delete branch!");
                }
            }
            return View();
        }
    }
}
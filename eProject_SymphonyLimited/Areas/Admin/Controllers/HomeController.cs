using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        SymphonyLimitedDBContext db;

        public HomeController()
        {
            db = new SymphonyLimitedDBContext();
        }

        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserProfile(int id)
        {
            return View(db.User.Find(id));
        }

        public ActionResult UserProfileEdit(int id)
        {
            var findGroupId = db.User.Find(id).GroupId;
            var user = db.User.FirstOrDefault(x => x.EntityId == id);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("UserProfile", new { id = id });
        }

        [HttpPost]
        public ActionResult UserProfileEdit(UserViewModel u, HttpPostedFileBase imgFile)
        {
            var currentUser = db.User.Find(u.EntityId);
            var validateFullName = db.User.FirstOrDefault(x => x.FullName != currentUser.FullName && x.FullName == u.FullName);
            var validateEmail = db.User.FirstOrDefault(x => x.Email != currentUser.Email && x.Email == u.Email);
            var validatePhone = db.User.FirstOrDefault(x => x.Phone != currentUser.Phone && x.Phone == u.Phone);
            if (validateFullName != null)
            {
                ModelState.AddModelError("FullName", "User full name can't be the same!");
            }
            if (validateEmail != null)
            {
                ModelState.AddModelError("Email", "User email can't be the same!");
            }
            if (validatePhone != null)
            {
                ModelState.AddModelError("Phone", "User phone can't be the same!");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    currentUser.FullName = u.FullName;
                    currentUser.Email = u.Email;
                    currentUser.Phone = u.Phone;
                    currentUser.Address = u.Address;
                    if (imgFile != null)
                    {
                        string imgName = Path.GetFileName(imgFile.FileName);
                        string imgText = Path.GetExtension(imgName);
                        string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                        currentUser.Image = imgName;
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
                    return RedirectToAction("UserProfile", new { id = u.EntityId });

                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save user!");
                }
            }
            return View(currentUser);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel cpvm)
        {
            var currentAccount = Session["UserAccount"].ToString();
            var findUser = db.User.FirstOrDefault(x => x.Account.Equals(currentAccount));
            if (ModelState.IsValid)
            {
                if (findUser.Password.Equals(cpvm.OldPassword))
                {
                    findUser.Password = cpvm.NewPassword;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Change successful!";
                }
                else
                {
                    TempData["ErrorMessage"] = "Your password is wrong";
                }
            }
            return View();
        }
    }
}
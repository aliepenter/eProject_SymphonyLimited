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
            var u = db.User.Find(id);
            UserViewModel uvm = new UserViewModel
            {
                EntityId = u.EntityId,
                Account = u.Account,
                Password = u.Password,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                Image = u.Image,
                GroupId = u.GroupId,
                GroupUserName = u.GroupUsers.Name
            };
            return View(uvm);
        }

        [HttpPost]
        public ActionResult UserProfile(UserViewModel uvm, HttpPostedFileBase imgFile)
        {
            var currentUser = db.User.Find(uvm.EntityId);
            var validateFullName = db.User.FirstOrDefault(x => x.FullName != currentUser.FullName && x.FullName == uvm.FullName);
            var validateEmail = db.User.FirstOrDefault(x => x.Email != currentUser.Email && x.Email == uvm.Email);
            var validatePhone = db.User.FirstOrDefault(x => x.Phone != currentUser.Phone && x.Phone == uvm.Phone);

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
                    currentUser.FullName = uvm.FullName;
                    currentUser.Email = uvm.Email;
                    currentUser.Phone = uvm.Phone;
                    if (imgFile != null)
                    {

                        string imgName = Path.GetFileName(imgFile.FileName);
                        string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                        currentUser.Image = imgName;
                        uvm.Image = imgName;
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
                    TempData["SuccessMessage"] = "Save Success!";
                    Session["User"] = currentUser;
                    Session["UserEntityId"] = currentUser.EntityId;
                    Session["UserAccount"] = currentUser.Account;
                    Session["UserName"] = currentUser.FullName;
                    Session["UserImage"] = currentUser.Image;
                    Session["UserRole"] = currentUser.GroupUsers.IsAdmin.ToString();
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save user!");
                }
            }
            return View(uvm);
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
                    if (!cpvm.OldPassword.Equals(cpvm.NewPassword))
                    {
                        findUser.Password = cpvm.NewPassword;
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Change successful!";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Your new password is same old password";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Your old password is wrong";
                }
            }
            return View();
        }
    }
}
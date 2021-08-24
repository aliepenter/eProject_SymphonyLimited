using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        SymphonyLimitedDBContext db;

        public AuthController()
        {
            db = new SymphonyLimitedDBContext();
        }

        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel l)
        {
            if (ModelState.IsValid)
            {
                var findUser = db.User.FirstOrDefault(x => x.Account == l.Account && x.Password == l.Password);
                if (findUser != null)
                {
                    Session["User"] = findUser;
                    Session["UserEntityId"] = findUser.EntityId;
                    Session["UserAccount"] = findUser.Account;
                    Session["UserName"] = findUser.FullName;
                    Session["UserImage"] = findUser.Image;
                    Session["UserRole"] = findUser.GroupUsers.IsAdmin.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Account or Password does not exist";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string Email)
        {
            var findUser = db.User.FirstOrDefault(x => x.Email == Email);
            if (findUser != null)
            {
                var link = "Click here to reset your password => " + Request.Url.Scheme + "://" + Request.Url.Authority + @Url.Action("ResetPassword", "Auth", new { id = findUser.EntityId });
                var senderEmail = new MailAddress("hoangcaolong2311@gmail.com", "Eternal Nightmare");
                var receiverEmail = new MailAddress(Email, "Receiver");
                var password = "Longdaica123";
                var smtp = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = "Confirm to reset password",
                    Body = link
                })
                {
                    try
                    {
                        mess.IsBodyHtml = true;
                        smtp.Send(mess);
                        return Json(new
                        {
                            StatusCode = 200,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    catch (Exception)
                    {
                        return Json(new
                        {
                            StatusCode = 400,
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else
            {
                return Json(new
                {
                    StatusCode = 500,
                }, JsonRequestBehavior.AllowGet);
            }
           
        }

        public ActionResult ResetPassword(int id)
        {
            var user = db.User.Find(id);
            ChangePasswordViewModel cpvm = new ChangePasswordViewModel();
            cpvm.EntityId = user.EntityId;
            cpvm.OldPassword = user.Password;
            return View(cpvm);
        }

        [HttpPost]
        public ActionResult ResetPassword(ChangePasswordViewModel cpvm)
        {
            var user = db.User.Find(cpvm.EntityId);
            if (ModelState.IsValid)
            {
                user.Password = cpvm.NewPassword;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Change successful!";
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult UnAuthorize()
        {
            return View();
        }
    }
}
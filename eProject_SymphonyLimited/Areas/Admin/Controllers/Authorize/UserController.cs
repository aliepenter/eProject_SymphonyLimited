using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using eProject_SymphonyLimited.Models.Authorize;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace eProject_SymphonyLimited.Areas.Admin.Controllers.Authorize
{
    [CustomizeAuthorize]
    public class UserController : BaseController
    {
        SymphonyLimitedDBContext db = new SymphonyLimitedDBContext();

        // GET: Admin/CoreConfigData
        public ActionResult Index()
        {
            return View(db.User.AsEnumerable());
        }

        [HttpGet]
        public ActionResult Get(int page = 1, string type = null, string key = null)
        {
            int pageSize = 5;
            var users = db.User.Join(db.GroupUser,
                u => u.GroupId,
                gu => gu.EntityId,
                (u, gu) => new
                UserViewModel
                {
                    EntityId = u.EntityId,
                    Account = u.Account,
                    FullName = u.FullName,
                    Email = u.Email,
                    Phone = u.Phone,
                    Image = u.Image,
                    GroupUserName = gu.Name
                }).AsEnumerable();
            if (!String.IsNullOrEmpty(type) && !String.IsNullOrEmpty(key))
            {
                switch (type)
                {
                    case "Account":
                        users = db.User.Join(db.GroupUser,
                            u => u.GroupId,
                            gu => gu.EntityId,
                            (u, gu) => new
                            UserViewModel
                            {
                                EntityId = u.EntityId,
                                Account = u.Account,
                                FullName = u.FullName,
                                Email = u.Email,
                                Phone = u.Phone,
                                Image = u.Image,
                                GroupUserName = gu.Name
                            }).Where(x => x.Account.ToString().Contains(key)).AsEnumerable();
                        break;
                    case "FullName":
                        users = (IQueryable<UserViewModel>)db.User.Join(db.GroupUser,
                             u => u.GroupId,
                             gu => gu.EntityId,
                             (u, gu) => new
                             UserViewModel
                             {
                                 EntityId = u.EntityId,
                                 Account = u.Account,
                                 FullName = u.FullName,
                                 Email = u.Email,
                                 Phone = u.Phone,
                                 Image = u.Image,
                                 GroupUserName = gu.Name
                             }).Where(x => x.FullName.Contains(key)).AsEnumerable();
                        break;
                    case "Email":
                        users = (IQueryable<UserViewModel>)db.User.Join(db.GroupUser,
                            u => u.GroupId,
                            gu => gu.EntityId,
                            (u, gu) => new
                            UserViewModel
                            {
                                EntityId = u.EntityId,
                                Account = u.Account,
                                FullName = u.FullName,
                                Email = u.Email,
                                Phone = u.Phone,
                                Image = u.Image,
                                GroupUserName = gu.Name
                            }).Where(x => x.Email.Contains(key)).AsEnumerable();
                        break;
                    case "GroupName":
                        users = (IQueryable<UserViewModel>)db.User.Join(db.GroupUser,
                            u => u.GroupId,
                            gu => gu.EntityId,
                            (u, gu) => new
                            UserViewModel
                            {
                                EntityId = u.EntityId,
                                Account = u.Account,
                                FullName = u.FullName,
                                Email = u.Email,
                                Phone = u.Phone,
                                Image = u.Image,
                                GroupUserName = gu.Name
                            }).Where(x => x.GroupUserName.Contains(key)).AsEnumerable();
                        break;
                    default:
                        break;
                }
            }
            decimal totalPages = Math.Ceiling((decimal)users.Count() / pageSize);
            string jsonData = JsonConvert.SerializeObject(users.Skip((page - 1) * pageSize).Take(pageSize));
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
            ViewBag.GroupId = new SelectList(db.GroupUser.AsEnumerable(), "EntityId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(User u, HttpPostedFileBase imgFile)
        {
            ViewBag.GroupId = new SelectList(db.GroupUser.AsEnumerable(), "EntityId", "Name", u.GroupId);
            var validateAccount = db.User.FirstOrDefault(x => x.Account == u.Account);
            var validateFullName = db.User.FirstOrDefault(x => x.FullName == u.FullName);
            var validateEmail = db.User.FirstOrDefault(x => x.Email == u.Email);
            var validatePhone = db.User.FirstOrDefault(x => x.Phone == u.Phone);
            if (validateAccount != null)
            {
                ModelState.AddModelError("Account", "User account can't be the same!");
            }
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
                    if (imgFile == null && u.Image == null)
                    {
                        u.Password = "1234";
                        u.Image = "undraw_profile.svg";
                        db.User.Add(u);
                        db.SaveChanges();
                    }
                    else
                    {
                        u.Password = "1234";
                        string imgName = Path.GetFileName(imgFile.FileName);
                        string imgText = Path.GetExtension(imgName);
                        string imgPath = Path.Combine(Server.MapPath("~/Areas/Admin/Content/img/"), imgName);
                        u.Image = imgName;
                        if (imgFile.ContentLength > 0)
                        {
                            db.User.Add(u);
                            if (db.SaveChanges() > 0)
                            {
                                imgFile.SaveAs(imgPath);
                                ViewBag.msg = "Record Added";
                                ModelState.Clear();
                            }
                        }
                    }
                    var senderEmail = new MailAddress("hoangcaolong2311@gmail.com", "Eternal Nightmare");
                    var receiverEmail = new MailAddress(u.Email, "Receiver");
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
                        Subject = "Symphony Limited ",
                        Body = "<div style=\"background: #fff; margin: 0 auto; width: 300px;    width: 500px; " +
                    "padding: 20px; border: 1px solid red;\">" +
                                "<div  style= \" text-align: left\">" +
                                    "<h1>Dear: " +u.FullName+ ",</h1>" +
                                    "<span>Your password is: <span style=\"font-weight: bold; font-size: 16px; color: red\">" + u.Password + " <br/></span>" +
                                    "<span>Thank you,<br/></span>" +
                                    "<span style = \"font-weight: bold\">Symphony Limited</span>" +
                                "</div>" +
                            "</div>"

                    })
                    {
                        try
                        {
                            mess.IsBodyHtml = true;
                            smtp.Send(mess);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save user!");
                }
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            var findGroupId = db.User.Find(id).GroupId;
            ViewBag.GroupId = new SelectList(db.GroupUser.AsEnumerable(), "EntityId", "Name", findGroupId);
            var user = db.User.FirstOrDefault(x => x.EntityId == id);
            if (user != null)
            {
                return View(user);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(User u, HttpPostedFileBase imgFile)
        {
            ViewBag.GroupId = new SelectList(db.GroupUser.AsEnumerable(), "EntityId", "Name", u.GroupId);
            var currentUser = db.User.Find(u.EntityId);
            var validateAccount = db.User.FirstOrDefault(x => x.Account != currentUser.Account && x.Account == u.Account);
            var validateFullName = db.User.FirstOrDefault(x => x.FullName != currentUser.FullName && x.FullName == u.FullName);
            var validateEmail = db.User.FirstOrDefault(x => x.Email != currentUser.Email && x.Email == u.Email);
            var validatePhone = db.User.FirstOrDefault(x => x.Phone != currentUser.Phone && x.Phone == u.Phone);
            if (validateAccount != null)
            {
                ModelState.AddModelError("Account", "User account can't be the same!");
            }
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
                    currentUser.Account = u.Account;
                    currentUser.FullName = u.FullName;
                    currentUser.Email = u.Email;
                    currentUser.Phone = u.Phone;
                    currentUser.GroupId = u.GroupId;
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
                    return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while save user!");
                }
            }
            return View(currentUser);
        }

        public ActionResult Delete(int id)
        {
            if (db.User.Find(id) != null)
            {
                try
                {
                    db.User.Remove(db.User.Find(id));
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Some thing went wrong while delete user!");
                }
            }
            return View();
        }
    }
}
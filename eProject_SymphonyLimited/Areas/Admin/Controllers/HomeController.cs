using eProject_SymphonyLimited.Areas.Admin.Data;
using eProject_SymphonyLimited.Areas.Admin.Data.ViewModel;
using eProject_SymphonyLimited.Models;
using System.Linq;
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

        public ActionResult UnAuthorize()
        {
            return View();
        }
    }
}
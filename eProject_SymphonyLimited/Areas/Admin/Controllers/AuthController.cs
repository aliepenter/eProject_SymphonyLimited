using eProject_SymphonyLimited.Areas.Admin.Data.Model;
using eProject_SymphonyLimited.Models;
using System.Linq;
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
            if (!Session["User"].Equals(""))
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
                    Session["AdminName"] = findUser.FullName;
                    Session["AdminImage"] = findUser.Image;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "Account or Password does not exist";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session["User"] = "";
            return RedirectToAction("Index", "Home");
        }
    }
}
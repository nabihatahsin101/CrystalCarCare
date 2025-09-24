using System.Linq;
using System.Web.Mvc;
using CrystalCarCare.Models;

namespace CrystalCarCare.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Please fill in all fields.";
                return View(model);
            }

            using (var db = new UserDbContext())
            {
                var admin = db.Admins
                              .FirstOrDefault(a => a.Username == model.Username && a.Password == model.Password);

                if (admin != null)
                {
                    // ✅ Store both username and flag
                    Session["AdminUsername"] = admin.Username;
                    Session["IsAdmin"] = true;

                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ViewBag.Error = "Invalid username or password.";
                    return View(model);
                }
            }
        }

        // GET: Admin/Dashboard
        public ActionResult Dashboard()
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            return View();
        }

        // GET: Admin/Logout
        public ActionResult Logout()
        {
            Session.Clear(); // ✅ clears both AdminUsername and IsAdmin
            return RedirectToAction("Login");
        }
    }
}

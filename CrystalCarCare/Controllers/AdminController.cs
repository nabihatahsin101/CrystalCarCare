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
            if (ModelState.IsValid)
            {
                // Hardcoded credentials for now
                if (model.Username.Trim() == "admin" && model.Password.Trim() == "admin123")
                {
                    // Store admin login in session
                    Session["IsAdmin"] = true;
                    Session["AdminName"] = model.Username;

                    return RedirectToAction("Dashboard");
                }

                ViewBag.Error = "Invalid username or password!";
            }

            return View(model);
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
            Session["IsAdmin"] = null;
            Session["AdminName"] = null;
            return RedirectToAction("Login");
        }
    }
}

using CrystalCarCare.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace CrystalCarCare.Controllers
{
    public class AccountController : Controller
    {
        private UserDbContext db = new UserDbContext();

        // GET: Signup
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(UserRegister user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                ViewBag.Message = "User registered successfully!";
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserRegister login)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
            if (user != null)
            {
                // Set authentication cookie
                System.Web.Security.FormsAuthentication.SetAuthCookie(user.Email, false);

                // Redirect to homepage (or dashboard)
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = "Invalid email or password.";
                return View(login);
            }
        }
        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


    }
}

using CrystalCarCare.Models;
using System.Linq;
using System.Web.Mvc;

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
                ViewBag.Message = "Login successful!";
                return View();
            }
            else
            {
                ViewBag.Message = "Invalid email or password.";
                return View(login);
            }
        }
    }
}

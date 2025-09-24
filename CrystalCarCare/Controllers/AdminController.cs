using CrystalCarCare.Models;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

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
                    // store both username and flag (keep consistent with existing Dashboard view)
                    Session["AdminUsername"] = admin.Username;
                    Session["AdminName"] = admin.Username; // some views referenced AdminName earlier
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
            Session.Clear();
            return RedirectToAction("Login");
        }

        // -------------------------
        // User management actions
        // -------------------------

        // GET: Admin/Users
        public ActionResult Users()
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            using (var db = new UserDbContext())
            {
                // Order by UserId for deterministic list
                var users = db.Users.OrderBy(u => u.UserId).ToList();
                return View(users);
            }
        }

        // GET: Admin/DeleteUser/5
        public ActionResult DeleteUser(int? id)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            using (var db = new UserDbContext())
            {
                var user = db.Users.Find(id);
                if (user == null)
                    return HttpNotFound();

                return View(user);
            }
        }

        // POST: Admin/DeleteUser/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUserConfirmed(int id)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            using (var db = new UserDbContext())
            {
                var user = db.Users.Find(id);
                if (user == null)
                    return HttpNotFound();

                db.Users.Remove(user);
                db.SaveChanges();
            }

            TempData["Success"] = "User deleted successfully.";
            return RedirectToAction("Users");
        }
        // GET: Admin/ListCars
        public ActionResult ListCars()
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            using (var db = new UserDbContext())
            {
                var cars = db.Cars.ToList();
                return View(cars);
            }
        }

        // GET: Admin/AddCar
        public ActionResult AddCar()
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            return View();
        }

        // POST: Admin/AddCar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCar(Car model, HttpPostedFileBase ImageFile)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            if (!ModelState.IsValid)
                return View(model);

            using (var db = new UserDbContext())
            {
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(ImageFile.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                    ImageFile.SaveAs(path);
                    model.ImageUrl = "~/Content/img/" + fileName;
                }

                db.Cars.Add(model);
                db.SaveChanges();
            }

            TempData["Success"] = "Car added successfully!";
            return RedirectToAction("ListCars");
        }

        // GET: Admin/EditCar/5
        public ActionResult EditCar(int id)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            using (var db = new UserDbContext())
            {
                var car = db.Cars.Find(id);
                if (car == null) return HttpNotFound();
                return View(car);
            }
        }

        // POST: Admin/EditCar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCar(Car model, HttpPostedFileBase ImageFile)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            using (var db = new UserDbContext())
            {
                var car = db.Cars.Find(model.Id);
                if (car == null) return HttpNotFound();

                car.Make = model.Make;
                car.Model = model.Model;
                car.Year = model.Year;
                car.RentalPricePerDay = model.RentalPricePerDay;
                car.IsAvailable = model.IsAvailable;

                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    var fileName = System.IO.Path.GetFileName(ImageFile.FileName);
                    var path = System.IO.Path.Combine(Server.MapPath("~/Content/img/"), fileName);
                    ImageFile.SaveAs(path);
                    car.ImageUrl = "~/Content/img/" + fileName;
                }

                db.SaveChanges();
            }

            TempData["Success"] = "Car updated successfully!";
            return RedirectToAction("ListCars");
        }

        // GET: Admin/DeleteCar/5
        public ActionResult DeleteCar(int id)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            using (var db = new UserDbContext())
            {
                var car = db.Cars.Find(id);
                if (car == null) return HttpNotFound();
                return View(car);
            }
        }

        // POST: Admin/DeleteCar/5
        [HttpPost, ActionName("DeleteCar")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCarConfirmed(int id)
        {
            if (Session["IsAdmin"] == null || !(bool)Session["IsAdmin"])
                return RedirectToAction("Login");

            using (var db = new UserDbContext())
            {
                var car = db.Cars.Find(id);
                if (car == null) return HttpNotFound();
                db.Cars.Remove(car);
                db.SaveChanges();
            }

            TempData["Success"] = "Car deleted successfully!";
            return RedirectToAction("ListCars");
        }

    }
}

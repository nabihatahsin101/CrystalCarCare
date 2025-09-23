using CrystalCarCare.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;

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
        [HttpGet]
        public ActionResult Booking(string serviceName)
        {
            ViewBag.ServiceName = serviceName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Booking(Booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.UserId = User.Identity.GetUserId();
                db.Bookings.Add(booking);
                db.SaveChanges();
                TempData["Message"] = "Your booking has been successfully submitted!";
                return RedirectToAction("BookingConfirmation", new { bookingId = booking.BookingId });

            }

            return View(booking);
        }

        public ActionResult BookingConfirmation(int bookingId)
        {
            var booking = db.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            if (booking == null)
            {
                return HttpNotFound();
            }

            var user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

            var model = new BookingConfirmationViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                Booking = booking
            };

            return View(model);
        }

        // 🔹 PDF Generator
        public FileResult DownloadConfirmation(int bookingId)
        {
            var booking = db.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            var user = db.Users.FirstOrDefault(u => u.Email == User.Identity.Name);

            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document();
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                doc.Add(new Paragraph("Booking Confirmation"));
                doc.Add(new Paragraph($"Name: {user.UserName}"));
                doc.Add(new Paragraph($"Email: {user.Email}"));
                doc.Add(new Paragraph($"Service Type: {booking.ServiceType}"));
                doc.Add(new Paragraph($"Date: {booking.Date.ToShortDateString()}"));
                doc.Add(new Paragraph($"Time: {booking.Time}"));
                doc.Add(new Paragraph($"Vehicle: {booking.VehicleYear} {booking.VehicleMake} {booking.VehicleModel}"));
                doc.Add(new Paragraph($"License Plate: {booking.LicensePlate}"));
                if (!string.IsNullOrEmpty(booking.SpecialRequests))
                    doc.Add(new Paragraph($"Special Requests: {booking.SpecialRequests}"));

                doc.Close();

                return File(ms.ToArray(), "application/pdf", "BookingConfirmation.pdf");
            }
        }


    }
}

using System.Web.Mvc;
using CrystalCarCare.Models;   // ✅ Add this
using Microsoft.AspNet.Identity; // ✅ Add this if using Identity

namespace CrystalCarCare.Controllers
{
    public class UserController : Controller
    {
        private UserDbContext db = new UserDbContext();

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
                // If using Identity
                booking.UserId = User.Identity.GetUserId();

                // If NOT using Identity, replace with:
                // booking.UserId = User.Identity.Name;

                db.Bookings.Add(booking);
                db.SaveChanges();

                TempData["Message"] = "Your booking has been successfully submitted!";
                return RedirectToAction("BookingConfirmation");
            }

            return View(booking);
        }

        public ActionResult BookingConfirmation()
        {
            return View();
        }
    }
}

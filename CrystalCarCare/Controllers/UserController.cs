using System.Web.Mvc;
using CrystalCarCare.Models;
using Microsoft.AspNet.Identity;

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

                db.Bookings.Add(booking);
                db.SaveChanges();

                return Json(new { status = "Saved" }); // Only returns "Saved"
            }

            return Json(new { status = "Failed" });
        }
    }
}

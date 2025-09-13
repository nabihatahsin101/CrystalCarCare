using System.Web.Mvc;
using System.Collections.Generic;

namespace CrystalCarCare.Controllers
{
    // Move the BookingModel class to be a top-level class
    public class BookingModel
    {
        public string Id { get; set; }
        public string Service { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }

    public class UserController : Controller
    {
        // Static list to store bookings (in a real app, this would be a database)
        private static List<BookingModel> bookings = new List<BookingModel>
        {
            new BookingModel { Id = "001", Service = "Premium Wash", Date = "2025-09-02", Status = "Completed" },
            new BookingModel { Id = "002", Service = "Oil Change", Date = "2025-09-03", Status = "Pending" }
        };

        // GET: /User/BookingStatus
        public ActionResult BookingStatus()
        {
            // Pass the bookings list to the view
            ViewBag.Bookings = bookings;
            return View();
        }
        // GET: /User/BookNow
        public ActionResult Booking(string serviceName, decimal? price)
        {
            ViewBag.ServiceName = serviceName;
            ViewBag.Price = price ?? 0;
            return View("Booking"); // Renders Booking.cshtml
        }



        // POST: /User/Booking
        [HttpPost]
        public ActionResult Booking(FormCollection form)
        {
            // Create a new booking from form data
            var newBooking = new BookingModel
            {
                Id = GenerateBookingId(),
                Service = form["serviceType"],
                Date = form["date"],
                Status = "Confirmed"
            };

            // Add to the bookings list
            bookings.Add(newBooking);

            // Store in TempData to show confirmation message
            TempData["NewBooking"] = newBooking;

            // Redirect to booking status page
            return RedirectToAction("BookingStatus");
        }

        // Helper method to generate a unique booking ID
        private string GenerateBookingId()
        {
            return "BK" + (bookings.Count + 1).ToString("D3");
        }
    }
}
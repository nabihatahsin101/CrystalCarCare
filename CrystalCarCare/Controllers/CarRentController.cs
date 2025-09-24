using CrystalCarCare.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CrystalCarCare.Controllers
{
    public class CarRentController : Controller
    {
        private UserDbContext db = new UserDbContext();

        // GET: CarRent
        public ActionResult Index(string searchString = "", string sortOrder = "")
        {
            var cars = db.Cars.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                cars = cars.Where(c => c.Make.Contains(searchString) || c.Model.Contains(searchString));

            switch (sortOrder)
            {
                case "PriceAsc": cars = cars.OrderBy(c => c.RentalPricePerDay); break;
                case "PriceDesc": cars = cars.OrderByDescending(c => c.RentalPricePerDay); break;
                case "AlphaAsc": cars = cars.OrderBy(c => c.Make).ThenBy(c => c.Model); break;
                case "AlphaDesc": cars = cars.OrderByDescending(c => c.Make).ThenByDescending(c => c.Model); break;
                case "YearAsc": cars = cars.OrderBy(c => c.Year); break;
                case "YearDesc": cars = cars.OrderByDescending(c => c.Year); break;
                default: cars = cars.OrderBy(c => c.Make); break;
            }

            return View(cars.ToList());
        }

        // GET: CarRent/Rent/5
        public ActionResult Rent(int id)
        {
            var car = db.Cars.Find(id);
            if (car == null || !car.IsAvailable)
                return HttpNotFound("Car not available");

            return View(car);
        }

        // POST: CarRent/Book
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(int id, DateTime date, string time, string specialRequests)
        {
            var car = db.Cars.Find(id);
            if (car == null || !car.IsAvailable)
                return HttpNotFound("Car not available");

            // mark car unavailable
            car.IsAvailable = false;

            // create booking according to teammate's model
            var booking = new Booking
            {
                ServiceType = "Car Rental",
                Date = date,
                Time = time,
                VehicleMake = car.Make,
                VehicleModel = car.Model,
                VehicleYear = car.Year,
                LicensePlate = "N/A", // or you can add a LicensePlate field in Car if available
                SpecialRequests = specialRequests,
                UserId = Session["UserId"]?.ToString() ?? "Guest"
            };

            db.Bookings.Add(booking);
            db.SaveChanges();

            TempData["Success"] = $"Booked {car.Make} {car.Model} successfully!";
            return RedirectToAction("Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CrystalCarCare.Models;

namespace CrystalCarCare.Controllers
{
    [Authorize]
    public class CarRentController : Controller
    {
        private static List<Car> cars = new List<Car>
        {
            new Car { Id = 1, Make = "Toyota", Model = "Camry", Year = 2022, RentalPricePerDay = 50.00m, ImageUrl = "~/Content/img/toyota_camry.jpg", IsAvailable = true },
            new Car { Id = 2, Make = "Honda", Model = "Civic", Year = 2021, RentalPricePerDay = 45.00m, ImageUrl = "~/Content/img/honda_civic.jpg", IsAvailable = false },
            new Car { Id = 3, Make = "Ford", Model = "Mustang", Year = 2023, RentalPricePerDay = 80.00m, ImageUrl = "~/Content/img/ford_mustang.jpg", IsAvailable = true },
            new Car { Id = 4, Make = "Tesla", Model = "Model 3", Year = 2024, RentalPricePerDay = 100.00m, ImageUrl = "~/Content/img/tesla_model_3.jpg", IsAvailable = true }
        };

        public ActionResult Index(string searchString = "", string sortOrder = "")
        {
            ViewBag.SearchString = searchString ?? "";
            ViewBag.CurrentSort = sortOrder ?? "AlphaAsc";

            var filteredCars = cars.AsQueryable();

            // Search
            if (!string.IsNullOrEmpty(searchString))
            {
                filteredCars = filteredCars.Where(c =>
                    c.Make.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    c.Model.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                );
            }

            // Sorting
            switch (sortOrder)
            {
                case "PriceAsc":
                    filteredCars = filteredCars.OrderBy(c => c.RentalPricePerDay);
                    break;
                case "PriceDesc":
                    filteredCars = filteredCars.OrderByDescending(c => c.RentalPricePerDay);
                    break;
                case "AlphaAsc":
                    filteredCars = filteredCars.OrderBy(c => c.Make).ThenBy(c => c.Model);
                    break;
                case "AlphaDesc":
                    filteredCars = filteredCars.OrderByDescending(c => c.Make).ThenByDescending(c => c.Model);
                    break;
                case "YearAsc":
                    filteredCars = filteredCars.OrderBy(c => c.Year);
                    break;
                case "YearDesc":
                    filteredCars = filteredCars.OrderByDescending(c => c.Year);
                    break;
                default:
                    filteredCars = filteredCars.OrderBy(c => c.Make).ThenBy(c => c.Model);
                    break;
            }

            return View(filteredCars.ToList());
        }

        public ActionResult Rent(int id)
        {
            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car == null || !car.IsAvailable)
                return HttpNotFound("Car not found or not available.");

            return View(car);
        }

        [HttpPost]
        public ActionResult Book(int carId, int rentalDays, DateTime startDate)
        {
            var car = cars.FirstOrDefault(c => c.Id == carId);
            if (car == null || !car.IsAvailable)
                return HttpNotFound("Car not found or not available.");

            car.IsAvailable = false; // mark as booked for demo
            TempData["Success"] = $"Booked {car.Make} {car.Model} for {rentalDays} day(s) starting {startDate:d}.";
            return RedirectToAction("Index");
        }
    }
}

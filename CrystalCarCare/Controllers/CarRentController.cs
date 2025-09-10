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
        // Sample data (replace with database in production)
        private static List<Car> cars = new List<Car>
        {
            new Car { Id = 1, Make = "Toyota", Model = "Camry", Year = 2022, RentalPricePerDay = 50.00m, ImageUrl = "~/Content/img/toyota_camry.jpg", IsAvailable = true },
            new Car { Id = 2, Make = "Honda", Model = "Civic", Year = 2021, RentalPricePerDay = 45.00m, ImageUrl = "~/Content/img/honda_civic.jpg", IsAvailable = false },
            new Car { Id = 3, Make = "Ford", Model = "Mustang", Year = 2023, RentalPricePerDay = 80.00m, ImageUrl = "~/Content/img/ford_mustang.jpg", IsAvailable = true },
            new Car { Id = 4, Make = "Tesla", Model = "Model 3", Year = 2024, RentalPricePerDay = 100.00m, ImageUrl = "~/Content/img/tesla_model_3.jpg", IsAvailable = true }
        };

        public ActionResult Index(string searchString = "", string sortOrder = "")
        {
            ViewBag.SearchString = searchString;
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";

            var filteredCars = cars.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                filteredCars = filteredCars.Where(c => c.Make.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                                      c.Model.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            switch (sortOrder)
            {
                case "Price":
                    filteredCars = filteredCars.OrderBy(c => c.RentalPricePerDay);
                    break;
                case "price_desc":
                    filteredCars = filteredCars.OrderByDescending(c => c.RentalPricePerDay);
                    break;
                case "Year":
                    filteredCars = filteredCars.OrderBy(c => c.Year);
                    break;
                case "year_desc":
                    filteredCars = filteredCars.OrderByDescending(c => c.Year);
                    break;
                default:
                    filteredCars = filteredCars.OrderBy(c => c.Make);
                    break;
            }

            return View(filteredCars.ToList());
        }

        public ActionResult Rent(int id)
        {
            var car = cars.FirstOrDefault(c => c.Id == id);
            if (car == null || !car.IsAvailable)
            {
                return HttpNotFound("Car not found or not available for rent.");
            }
            return View(car);
        }
    }
}
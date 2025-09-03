// File: Models/Car.cs
using System;

namespace CrystalCarCare.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal RentalPricePerDay { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
    }
}
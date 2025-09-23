using System;
using System.ComponentModel.DataAnnotations;

namespace CrystalCarCare.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public string ServiceType { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Time { get; set; }

        [Required]
        public string VehicleMake { get; set; }

        [Required]
        public string VehicleModel { get; set; }

        [Required]
        public int VehicleYear { get; set; }

        [Required]
        public string LicensePlate { get; set; }

        public string SpecialRequests { get; set; }

        public string UserId { get; set; } // to link booking to logged-in user
    }
}

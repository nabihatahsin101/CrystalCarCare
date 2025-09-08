using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrystalCarCare.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; } // Auto-generated unique ID

        [Required]
        public string ServiceName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
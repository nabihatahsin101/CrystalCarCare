using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrystalCarCare.Models
{
    public class BookingConfirmationViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public Booking Booking { get; set; }
    }
}
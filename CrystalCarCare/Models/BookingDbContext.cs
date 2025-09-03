using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace CrystalCarCare.Models
{
    public class BookingDbContext : DbContext
    {
        public BookingDbContext() : base("UserDbContext") { }

        public DbSet<Booking> Bookings { get; set; }
    }
}
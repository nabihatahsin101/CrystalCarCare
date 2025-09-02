using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace CrystalCarCare.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() : base("name=UserDbContext") { }

        public DbSet<UserRegister> Users { get; set; }
    }
}
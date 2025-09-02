namespace CrystalCarCare.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CrystalCarCare.Models.UserDbContext>
    {
       
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;  // <-- enable automatic migration
            AutomaticMigrationDataLossAllowed = true; // optional: allows changes that might delete columns
        }

        protected override void Seed(CrystalCarCare.Models.UserDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

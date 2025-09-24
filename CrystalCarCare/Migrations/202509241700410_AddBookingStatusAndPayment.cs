namespace CrystalCarCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingStatusAndPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "Progress", c => c.String());
            AddColumn("dbo.Bookings", "PaymentStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "PaymentStatus");
            DropColumn("dbo.Bookings", "Progress");
        }
    }
}

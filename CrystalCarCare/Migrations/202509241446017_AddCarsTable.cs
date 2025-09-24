namespace CrystalCarCare.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Make = c.String(),
                        Model = c.String(),
                        Year = c.Int(nullable: false),
                        RentalPricePerDay = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImageUrl = c.String(),
                        IsAvailable = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cars");
        }
    }
}

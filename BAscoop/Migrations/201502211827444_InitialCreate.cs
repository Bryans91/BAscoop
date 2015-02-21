namespace BAscoop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nrOfTickets = c.Int(nullable: false),
                        accountNumber = c.String(),
                        totalPrice = c.Int(nullable: false),
                        adres = c.String(),
                        city = c.String(),
                        postal = c.String(),
                        discount_id = c.Int(),
                        guests_id = c.Int(),
                        performance_PerformanceId = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Discounts", t => t.discount_id)
                .ForeignKey("dbo.Guests", t => t.guests_id)
                .ForeignKey("dbo.Performances", t => t.performance_PerformanceId)
                .Index(t => t.discount_id)
                .Index(t => t.guests_id)
                .Index(t => t.performance_PerformanceId);
            
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        percentage = c.Int(nullable: false),
                        code = c.String(),
                        startDate = c.DateTime(nullable: false),
                        endDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        firstName = c.String(),
                        suffix = c.String(),
                        lastName = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Performances",
                c => new
                    {
                        PerformanceId = c.Int(nullable: false, identity: true),
                        StartTijd = c.DateTime(nullable: false),
                        MovieId = c.Int(nullable: false),
                        CinemaroomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PerformanceId)
                .ForeignKey("dbo.Cinemarooms", t => t.CinemaroomId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.CinemaroomId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.Cinemarooms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        number = c.Int(nullable: false),
                        capacity = c.Int(nullable: false),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        price = c.Int(nullable: false),
                        title = c.String(),
                        description = c.String(),
                        duration = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "performance_PerformanceId", "dbo.Performances");
            DropForeignKey("dbo.Performances", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.Performances", "CinemaroomId", "dbo.Cinemarooms");
            DropForeignKey("dbo.Bookings", "guests_id", "dbo.Guests");
            DropForeignKey("dbo.Bookings", "discount_id", "dbo.Discounts");
            DropIndex("dbo.Bookings", new[] { "performance_PerformanceId" });
            DropIndex("dbo.Performances", new[] { "MovieId" });
            DropIndex("dbo.Performances", new[] { "CinemaroomId" });
            DropIndex("dbo.Bookings", new[] { "guests_id" });
            DropIndex("dbo.Bookings", new[] { "discount_id" });
            DropTable("dbo.Movies");
            DropTable("dbo.Cinemarooms");
            DropTable("dbo.Performances");
            DropTable("dbo.Guests");
            DropTable("dbo.Discounts");
            DropTable("dbo.Bookings");
        }
    }
}

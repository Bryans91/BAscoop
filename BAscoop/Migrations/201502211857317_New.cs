namespace BAscoop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "guests_id", "dbo.Guests");
            DropForeignKey("dbo.Bookings", "discount_id", "dbo.Discounts");
            DropForeignKey("dbo.Bookings", "performance_PerformanceId", "dbo.Performances");
            DropIndex("dbo.Bookings", new[] { "guests_id" });
            DropIndex("dbo.Bookings", new[] { "discount_id" });
            DropIndex("dbo.Bookings", new[] { "performance_PerformanceId" });
            RenameColumn(table: "dbo.Bookings", name: "discount_id", newName: "DiscountId");
            RenameColumn(table: "dbo.Bookings", name: "performance_PerformanceId", newName: "PerformanceId");
            AddColumn("dbo.Bookings", "guestId", c => c.Int(nullable: false));
            AddColumn("dbo.Discounts", "StartTijd", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Bookings", "DiscountId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "PerformanceId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "guestId");
            CreateIndex("dbo.Bookings", "DiscountId");
            CreateIndex("dbo.Bookings", "PerformanceId");
            AddForeignKey("dbo.Bookings", "guestId", "dbo.Guests", "id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "DiscountId", "dbo.Discounts", "id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "PerformanceId", "dbo.Performances", "PerformanceId", cascadeDelete: true);
            DropColumn("dbo.Bookings", "guests_id");
            DropColumn("dbo.Discounts", "startDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discounts", "startDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookings", "guests_id", c => c.Int());
            DropForeignKey("dbo.Bookings", "PerformanceId", "dbo.Performances");
            DropForeignKey("dbo.Bookings", "DiscountId", "dbo.Discounts");
            DropForeignKey("dbo.Bookings", "guestId", "dbo.Guests");
            DropIndex("dbo.Bookings", new[] { "PerformanceId" });
            DropIndex("dbo.Bookings", new[] { "DiscountId" });
            DropIndex("dbo.Bookings", new[] { "guestId" });
            AlterColumn("dbo.Bookings", "PerformanceId", c => c.Int());
            AlterColumn("dbo.Bookings", "DiscountId", c => c.Int());
            DropColumn("dbo.Discounts", "StartTijd");
            DropColumn("dbo.Bookings", "guestId");
            RenameColumn(table: "dbo.Bookings", name: "PerformanceId", newName: "performance_PerformanceId");
            RenameColumn(table: "dbo.Bookings", name: "DiscountId", newName: "discount_id");
            CreateIndex("dbo.Bookings", "performance_PerformanceId");
            CreateIndex("dbo.Bookings", "discount_id");
            CreateIndex("dbo.Bookings", "guests_id");
            AddForeignKey("dbo.Bookings", "performance_PerformanceId", "dbo.Performances", "PerformanceId");
            AddForeignKey("dbo.Bookings", "discount_id", "dbo.Discounts", "id");
            AddForeignKey("dbo.Bookings", "guests_id", "dbo.Guests", "id");
        }
    }
}

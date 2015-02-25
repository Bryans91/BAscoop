namespace BAscoop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NAWtoGuest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "adres", c => c.String());
            AddColumn("dbo.Guests", "city", c => c.String());
            AddColumn("dbo.Guests", "postal", c => c.String());
            AlterColumn("dbo.Bookings", "totalPrice", c => c.Double(nullable: false));
            DropColumn("dbo.Bookings", "adres");
            DropColumn("dbo.Bookings", "city");
            DropColumn("dbo.Bookings", "postal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "postal", c => c.String());
            AddColumn("dbo.Bookings", "city", c => c.String());
            AddColumn("dbo.Bookings", "adres", c => c.String());
            AlterColumn("dbo.Bookings", "totalPrice", c => c.Int(nullable: false));
            DropColumn("dbo.Guests", "postal");
            DropColumn("dbo.Guests", "city");
            DropColumn("dbo.Guests", "adres");
        }
    }
}

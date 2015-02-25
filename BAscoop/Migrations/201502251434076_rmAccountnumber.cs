namespace BAscoop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rmAccountnumber : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookings", "accountNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "accountNumber", c => c.String());
        }
    }
}

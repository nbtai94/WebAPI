namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "TotalPrice");
        }
    }
}

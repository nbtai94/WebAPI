namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderCodeManagers", "Index", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "Index");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Index", c => c.Int(nullable: false));
            DropColumn("dbo.OrderCodeManagers", "Index");
        }
    }
}


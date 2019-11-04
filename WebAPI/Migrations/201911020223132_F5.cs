namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F5 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderCodeManagers", "FinalValueOfIndex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderCodeManagers", "FinalValueOfIndex", c => c.Int(nullable: false));
        }
    }
}

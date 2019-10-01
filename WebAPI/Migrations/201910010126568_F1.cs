namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropColumn("dbo.Orders", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ProductId");
            AddForeignKey("dbo.Orders", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}

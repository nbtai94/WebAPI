namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            AddColumn("dbo.OrderDetails", "Order_Id", c => c.Int());
            AddColumn("dbo.OrderDetails", "Order_Id1", c => c.Int());
            CreateIndex("dbo.OrderDetails", "Order_Id");
            CreateIndex("dbo.OrderDetails", "Order_Id1");
            AddForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders", "Id");
            AddForeignKey("dbo.OrderDetails", "Order_Id1", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "Order_Id1", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "Order_Id", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_Id1" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id" });
            DropColumn("dbo.OrderDetails", "Order_Id1");
            DropColumn("dbo.OrderDetails", "Order_Id");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}

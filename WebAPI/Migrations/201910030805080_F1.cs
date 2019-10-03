namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "Order_Id1", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "Order_Id1" });
            RenameColumn(table: "dbo.OrderDetails", name: "ProductId", newName: "Product_Id");
            AlterColumn("dbo.OrderDetails", "Product_Id", c => c.Int());
            CreateIndex("dbo.OrderDetails", "Product_Id");
            AddForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Products", "Id");
            DropColumn("dbo.OrderDetails", "OrderId");
            DropColumn("dbo.OrderDetails", "Order_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Order_Id1", c => c.Int());
            AddColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            AlterColumn("dbo.OrderDetails", "Product_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.OrderDetails", name: "Product_Id", newName: "ProductId");
            CreateIndex("dbo.OrderDetails", "Order_Id1");
            CreateIndex("dbo.OrderDetails", "OrderId");
            CreateIndex("dbo.OrderDetails", "ProductId");
            AddForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "Order_Id1", "dbo.Orders", "Id");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
        }
    }
}

namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Products");
            DropIndex("dbo.OrderDetails", new[] { "Product_Id" });
            RenameColumn(table: "dbo.OrderDetails", name: "Product_Id", newName: "ProductId");
            AddColumn("dbo.OrderDetails", "OrderId", c => c.Int(nullable: false));
            AddColumn("dbo.OrderDetails", "Order_Id1", c => c.Int());
            AlterColumn("dbo.OrderDetails", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "ProductId");
            CreateIndex("dbo.OrderDetails", "OrderId");
            CreateIndex("dbo.OrderDetails", "Order_Id1");
            AddForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders", "Id", cascadeDelete: true);
            AddForeignKey("dbo.OrderDetails", "Order_Id1", "dbo.Orders", "Id");
            AddForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "Order_Id1", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "Order_Id1" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            AlterColumn("dbo.OrderDetails", "ProductId", c => c.Int());
            DropColumn("dbo.OrderDetails", "Order_Id1");
            DropColumn("dbo.OrderDetails", "OrderId");
            RenameColumn(table: "dbo.OrderDetails", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.OrderDetails", "Product_Id");
            AddForeignKey("dbo.OrderDetails", "Product_Id", "dbo.Products", "Id");
        }
    }
}

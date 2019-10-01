namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.Orders", "TotalQuantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "DateOrder", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropColumn("dbo.Orders", "Note");
            DropColumn("dbo.Orders", "DateCreated");
            DropColumn("dbo.Orders", "DateOrder");
            DropColumn("dbo.Orders", "TotalQuantity");
            DropTable("dbo.OrderDetails");
        }
    }
}

namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryCodeManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryCodeDefine = c.String(),
                        Prefix = c.String(),
                        DateFormat = c.String(),
                        NumberOfZeroInNumber = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerCodeManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerCodeDefine = c.String(),
                        Prefix = c.String(),
                        DateFormat = c.String(),
                        NumberOfZeroInNumber = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCodeManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCodeDefine = c.String(),
                        Prefix = c.String(),
                        DateFormat = c.String(),
                        NumberOfZeroInNumber = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "CustomerCode", c => c.String());
            AddColumn("dbo.Customers", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Products", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.OrderCodeManagers", "OrderCodeDefine", c => c.String());
            AddColumn("dbo.OrderCodeManagers", "DateFormat", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderCodeManagers", "DateFormat");
            DropColumn("dbo.OrderCodeManagers", "OrderCodeDefine");
            DropColumn("dbo.Products", "CreateDate");
            DropColumn("dbo.Customers", "CreateDate");
            DropColumn("dbo.Customers", "CustomerCode");
            DropTable("dbo.ProductCodeManagers");
            DropTable("dbo.CustomerCodeManagers");
            DropTable("dbo.CategoryCodeManagers");
        }
    }
}

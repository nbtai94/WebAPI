namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CodeManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prefix = c.String(),
                        DateFormat = c.String(),
                        NumberOfZeroInNumber = c.Int(nullable: false),
                        Index = c.Int(nullable: false),
                        CodeDefine = c.String(),
                        Element = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.CategoryCodeManagers");
            DropTable("dbo.CustomerCodeManagers");
            DropTable("dbo.OrderCodeManagers");
            DropTable("dbo.ProductCodeManagers");
        }
        
        public override void Down()
        {
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
            
            CreateTable(
                "dbo.OrderCodeManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderCodeDefine = c.String(),
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
            
            DropTable("dbo.CodeManagers");
        }
    }
}

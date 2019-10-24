namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F3 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.ProductViewModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCode = c.String(),
                        Description = c.String(),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductCategoryId = c.Int(nullable: false),
                        ProductCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}

namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "NormalizeName", c => c.String());
            AddColumn("dbo.ProductCategories", "NormalizeCategoryName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductCategories", "NormalizeCategoryName");
            DropColumn("dbo.Products", "NormalizeName");
        }
    }
}

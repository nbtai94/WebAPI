namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "NormalizeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "NormalizeName");
        }
    }
}

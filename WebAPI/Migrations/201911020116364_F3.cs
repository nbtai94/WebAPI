namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderCodeManagers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Prefix = c.String(),
                        NumberOfZeroInNumber = c.Int(nullable: false),
                        FinalValueOfIndex = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "Index", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Index");
            DropTable("dbo.OrderCodeManagers");
        }
    }
}

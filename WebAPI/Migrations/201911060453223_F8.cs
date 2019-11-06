namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CodeManagers", "DateResetIndex", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CodeManagers", "DateResetIndex");
        }
    }
}

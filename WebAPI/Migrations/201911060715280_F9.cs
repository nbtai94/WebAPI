namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class F9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CodeManagers", "ResetIndex", c => c.Int(nullable: false));
            DropColumn("dbo.CodeManagers", "DateResetIndex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CodeManagers", "DateResetIndex", c => c.DateTime(nullable: false));
            DropColumn("dbo.CodeManagers", "ResetIndex");
        }
    }
}

namespace UNP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Shema_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "LocalStatus", c => c.String());
            AlterColumn("dbo.Users", "PortalStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PortalStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "LocalStatus", c => c.Int(nullable: false));
        }
    }
}

namespace PersonalManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doas", "Publish", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doas", "Publish");
        }
    }
}

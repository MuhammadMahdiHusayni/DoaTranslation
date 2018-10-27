namespace PersonalManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoaText : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Doas", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Doas", "Description");
        }
    }
}

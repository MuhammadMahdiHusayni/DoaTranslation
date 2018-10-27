namespace PersonalManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveLineNumber : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DoaTexts", "LineNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DoaTexts", "LineNumber", c => c.Int(nullable: false));
        }
    }
}

namespace PersonalManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDoaTextReal : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DoaTexts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LineNumber = c.Int(nullable: false),
                        Arabic = c.String(),
                        MalayTranslation = c.String(),
                        DoaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Doas", t => t.DoaId, cascadeDelete: true)
                .Index(t => t.DoaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DoaTexts", "DoaId", "dbo.Doas");
            DropIndex("dbo.DoaTexts", new[] { "DoaId" });
            DropTable("dbo.DoaTexts");
        }
    }
}

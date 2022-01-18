namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDislikedMediaTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DislikedMedia",
                c => new
                    {
                        UserRefId = c.Int(nullable: false),
                        MediaRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.MediaRefId })
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.MediaRefId, cascadeDelete: true)
                .Index(t => t.UserRefId)
                .Index(t => t.MediaRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DislikedMedia", "MediaRefId", "dbo.Media");
            DropForeignKey("dbo.DislikedMedia", "UserRefId", "dbo.Users");
            DropIndex("dbo.DislikedMedia", new[] { "MediaRefId" });
            DropIndex("dbo.DislikedMedia", new[] { "UserRefId" });
            DropTable("dbo.DislikedMedia");
        }
    }
}

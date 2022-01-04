namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationshipUsersLikes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LikedMovie", "LikedRefId", "dbo.Likeds");
            DropForeignKey("dbo.LikedMovie", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.LikedSeason", "LikedRefId", "dbo.Likeds");
            DropForeignKey("dbo.LikedSeason", "SeasonRefId", "dbo.Seasons");
            DropForeignKey("dbo.Likeds", "LikedId", "dbo.Users");
            DropIndex("dbo.Likeds", new[] { "LikedId" });
            DropIndex("dbo.LikedMovie", new[] { "LikedRefId" });
            DropIndex("dbo.LikedSeason", new[] { "LikedRefId" });
            CreateTable(
                "dbo.LikedMovie",
                c => new
                    {
                        UserRefId = c.Int(nullable: false),
                        MovieRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.MovieRefId })
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieRefId, cascadeDelete: true)
                .Index(t => t.UserRefId);
            
            CreateTable(
                "dbo.LikedSeason",
                c => new
                    {
                        UserRefId = c.Int(nullable: false),
                        SeasonRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.SeasonRefId })
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Seasons", t => t.SeasonRefId, cascadeDelete: true)
                .Index(t => t.UserRefId);
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.LikedSeason",
                c => new
                    {
                        LikedRefId = c.Int(nullable: false),
                        SeasonRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LikedRefId, t.SeasonRefId });
            
            CreateTable(
                "dbo.LikedMovie",
                c => new
                    {
                        LikedRefId = c.Int(nullable: false),
                        MovieRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LikedRefId, t.MovieRefId });
            
            CreateTable(
                "dbo.Likeds",
                c => new
                    {
                        LikedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikedId);
            
            DropForeignKey("dbo.LikedSeason", "SeasonRefId", "dbo.Seasons");
            DropForeignKey("dbo.LikedSeason", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.LikedMovie", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.LikedMovie", "UserRefId", "dbo.Users");
            DropIndex("dbo.LikedSeason", new[] { "UserRefId" });
            DropIndex("dbo.LikedMovie", new[] { "UserRefId" });
            DropTable("dbo.LikedSeason");
            DropTable("dbo.LikedMovie");
            CreateIndex("dbo.LikedSeason", "LikedRefId");
            CreateIndex("dbo.LikedMovie", "LikedRefId");
            CreateIndex("dbo.Likeds", "LikedId");
            AddForeignKey("dbo.Likeds", "LikedId", "dbo.Users", "Id");
            AddForeignKey("dbo.LikedSeason", "SeasonRefId", "dbo.Seasons", "SeasonId", cascadeDelete: true);
            AddForeignKey("dbo.LikedSeason", "LikedRefId", "dbo.Likeds", "LikedId", cascadeDelete: true);
            AddForeignKey("dbo.LikedMovie", "MovieRefId", "dbo.Movies", "MovieId", cascadeDelete: true);
            AddForeignKey("dbo.LikedMovie", "LikedRefId", "dbo.Likeds", "LikedId", cascadeDelete: true);
        }
    }
}

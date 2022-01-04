namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationshipForUserWatched : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WatchedMovie", "WatchedRefId", "dbo.Watched");
            DropForeignKey("dbo.WatchedMovie", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.WatchedSeason", "WatchedRefId", "dbo.Watched");
            DropForeignKey("dbo.WatchedSeason", "SeasonRefId", "dbo.Seasons");
            DropForeignKey("dbo.Watched", "WatchedId", "dbo.Users");
            DropIndex("dbo.Watched", new[] { "WatchedId" });
            DropIndex("dbo.WatchedMovie", new[] { "WatchedRefId" });
            DropIndex("dbo.WatchedSeason", new[] { "WatchedRefId" });
            CreateTable(
                "dbo.WatchedMovie",
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
                "dbo.WatchedSeason",
                c => new
                    {
                        UserRefId = c.Int(nullable: false),
                        SeasonRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.SeasonRefId })
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Seasons", t => t.SeasonRefId, cascadeDelete: true)
                .Index(t => t.UserRefId);
            
            DropTable("dbo.Watched");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WatchedSeason",
                c => new
                    {
                        WatchedRefId = c.Int(nullable: false),
                        SeasonRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WatchedRefId, t.SeasonRefId });
            
            CreateTable(
                "dbo.WatchedMovie",
                c => new
                    {
                        WatchedRefId = c.Int(nullable: false),
                        MovieRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WatchedRefId, t.MovieRefId });
            
            CreateTable(
                "dbo.Watched",
                c => new
                    {
                        WatchedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WatchedId);
            
            DropForeignKey("dbo.WatchedSeason", "SeasonRefId", "dbo.Seasons");
            DropForeignKey("dbo.WatchedSeason", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.WatchedMovie", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.WatchedMovie", "UserRefId", "dbo.Users");
            DropIndex("dbo.WatchedSeason", new[] { "UserRefId" });
            DropIndex("dbo.WatchedMovie", new[] { "UserRefId" });
            DropTable("dbo.WatchedSeason");
            DropTable("dbo.WatchedMovie");
            CreateIndex("dbo.WatchedSeason", "WatchedRefId");
            CreateIndex("dbo.WatchedMovie", "WatchedRefId");
            CreateIndex("dbo.Watched", "WatchedId");
            AddForeignKey("dbo.Watched", "WatchedId", "dbo.Users", "Id");
            AddForeignKey("dbo.WatchedSeason", "SeasonRefId", "dbo.Seasons", "SeasonId", cascadeDelete: true);
            AddForeignKey("dbo.WatchedSeason", "WatchedRefId", "dbo.Watched", "WatchedId", cascadeDelete: true);
            AddForeignKey("dbo.WatchedMovie", "MovieRefId", "dbo.Movies", "MovieId", cascadeDelete: true);
            AddForeignKey("dbo.WatchedMovie", "WatchedRefId", "dbo.Watched", "WatchedId", cascadeDelete: true);
        }
    }
}

namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLikedWatchedSeriesAndMoviesRelationship : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Likeds",
                c => new
                    {
                        LikedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikedId)
                .ForeignKey("dbo.Users", t => t.LikedId)
                .Index(t => t.LikedId);
            
            CreateTable(
                "dbo.Watched",
                c => new
                    {
                        WatchedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WatchedId)
                .ForeignKey("dbo.Users", t => t.WatchedId)
                .Index(t => t.WatchedId);
            
            CreateTable(
                "dbo.LikedMovie",
                c => new
                    {
                        LikedRefId = c.Int(nullable: false),
                        MovieRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LikedRefId, t.MovieRefId })
                .ForeignKey("dbo.Likeds", t => t.LikedRefId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieRefId, cascadeDelete: true)
                .Index(t => t.LikedRefId)
                .Index(t => t.MovieRefId);
            
            CreateTable(
                "dbo.LikedSeries",
                c => new
                    {
                        LikedRefId = c.Int(nullable: false),
                        SeriesRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LikedRefId, t.SeriesRefId })
                .ForeignKey("dbo.Likeds", t => t.LikedRefId, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.SeriesRefId, cascadeDelete: true)
                .Index(t => t.LikedRefId)
                .Index(t => t.SeriesRefId);
            
            CreateTable(
                "dbo.WatchedMovie",
                c => new
                    {
                        WatchedRefId = c.Int(nullable: false),
                        MovieRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WatchedRefId, t.MovieRefId })
                .ForeignKey("dbo.Watched", t => t.WatchedRefId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieRefId, cascadeDelete: true)
                .Index(t => t.WatchedRefId)
                .Index(t => t.MovieRefId);
            
            CreateTable(
                "dbo.WatchedSeries",
                c => new
                    {
                        WatchedRefId = c.Int(nullable: false),
                        SeriesRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WatchedRefId, t.SeriesRefId })
                .ForeignKey("dbo.Watched", t => t.WatchedRefId, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.SeriesRefId, cascadeDelete: true)
                .Index(t => t.WatchedRefId)
                .Index(t => t.SeriesRefId);
            
            DropColumn("dbo.Series", "StatisticId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Series", "StatisticId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Watched", "WatchedId", "dbo.Users");
            DropForeignKey("dbo.WatchedSeries", "SeriesRefId", "dbo.Series");
            DropForeignKey("dbo.WatchedSeries", "WatchedRefId", "dbo.Watched");
            DropForeignKey("dbo.WatchedMovie", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.WatchedMovie", "WatchedRefId", "dbo.Watched");
            DropForeignKey("dbo.Likeds", "LikedId", "dbo.Users");
            DropForeignKey("dbo.LikedSeries", "SeriesRefId", "dbo.Series");
            DropForeignKey("dbo.LikedSeries", "LikedRefId", "dbo.Likeds");
            DropForeignKey("dbo.LikedMovie", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.LikedMovie", "LikedRefId", "dbo.Likeds");
            DropIndex("dbo.WatchedSeries", new[] { "SeriesRefId" });
            DropIndex("dbo.WatchedSeries", new[] { "WatchedRefId" });
            DropIndex("dbo.WatchedMovie", new[] { "MovieRefId" });
            DropIndex("dbo.WatchedMovie", new[] { "WatchedRefId" });
            DropIndex("dbo.LikedSeries", new[] { "SeriesRefId" });
            DropIndex("dbo.LikedSeries", new[] { "LikedRefId" });
            DropIndex("dbo.LikedMovie", new[] { "MovieRefId" });
            DropIndex("dbo.LikedMovie", new[] { "LikedRefId" });
            DropIndex("dbo.Watched", new[] { "WatchedId" });
            DropIndex("dbo.Likeds", new[] { "LikedId" });
            DropTable("dbo.WatchedSeries");
            DropTable("dbo.WatchedMovie");
            DropTable("dbo.LikedSeries");
            DropTable("dbo.LikedMovie");
            DropTable("dbo.Watched");
            DropTable("dbo.Likeds");
        }
    }
}

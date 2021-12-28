namespace YMovies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedLikedWatchedRElationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LikedSeries", "LikedRefId", "dbo.Likeds");
            DropForeignKey("dbo.LikedSeries", "SeriesRefId", "dbo.Series");
            DropForeignKey("dbo.WatchedSeries", "WatchedRefId", "dbo.Watched");
            DropForeignKey("dbo.WatchedSeries", "SeriesRefId", "dbo.Series");
            DropIndex("dbo.LikedSeries", new[] { "LikedRefId" });
            DropIndex("dbo.LikedSeries", new[] { "SeriesRefId" });
            DropIndex("dbo.WatchedSeries", new[] { "WatchedRefId" });
            DropIndex("dbo.WatchedSeries", new[] { "SeriesRefId" });
            CreateTable(
                "dbo.LikedSeason",
                c => new
                    {
                        LikedRefId = c.Int(nullable: false),
                        SeasonRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LikedRefId, t.SeasonRefId })
                .ForeignKey("dbo.Likeds", t => t.LikedRefId, cascadeDelete: true)
                .ForeignKey("dbo.Seasons", t => t.SeasonRefId, cascadeDelete: true)
                .Index(t => t.LikedRefId)
                .Index(t => t.SeasonRefId);
            
            CreateTable(
                "dbo.WatchedSeason",
                c => new
                    {
                        WatchedRefId = c.Int(nullable: false),
                        SeasonRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WatchedRefId, t.SeasonRefId })
                .ForeignKey("dbo.Watched", t => t.WatchedRefId, cascadeDelete: true)
                .ForeignKey("dbo.Seasons", t => t.SeasonRefId, cascadeDelete: true)
                .Index(t => t.WatchedRefId)
                .Index(t => t.SeasonRefId);
            
            DropTable("dbo.LikedSeries");
            DropTable("dbo.WatchedSeries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WatchedSeries",
                c => new
                    {
                        WatchedRefId = c.Int(nullable: false),
                        SeriesRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WatchedRefId, t.SeriesRefId });
            
            CreateTable(
                "dbo.LikedSeries",
                c => new
                    {
                        LikedRefId = c.Int(nullable: false),
                        SeriesRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LikedRefId, t.SeriesRefId });
            
            DropForeignKey("dbo.WatchedSeason", "SeasonRefId", "dbo.Seasons");
            DropForeignKey("dbo.WatchedSeason", "WatchedRefId", "dbo.Watched");
            DropForeignKey("dbo.LikedSeason", "SeasonRefId", "dbo.Seasons");
            DropForeignKey("dbo.LikedSeason", "LikedRefId", "dbo.Likeds");
            DropIndex("dbo.WatchedSeason", new[] { "SeasonRefId" });
            DropIndex("dbo.WatchedSeason", new[] { "WatchedRefId" });
            DropIndex("dbo.LikedSeason", new[] { "SeasonRefId" });
            DropIndex("dbo.LikedSeason", new[] { "LikedRefId" });
            DropTable("dbo.WatchedSeason");
            DropTable("dbo.LikedSeason");
            CreateIndex("dbo.WatchedSeries", "SeriesRefId");
            CreateIndex("dbo.WatchedSeries", "WatchedRefId");
            CreateIndex("dbo.LikedSeries", "SeriesRefId");
            CreateIndex("dbo.LikedSeries", "LikedRefId");
            AddForeignKey("dbo.WatchedSeries", "SeriesRefId", "dbo.Series", "SeriesId", cascadeDelete: true);
            AddForeignKey("dbo.WatchedSeries", "WatchedRefId", "dbo.Watched", "WatchedId", cascadeDelete: true);
            AddForeignKey("dbo.LikedSeries", "SeriesRefId", "dbo.Series", "SeriesId", cascadeDelete: true);
            AddForeignKey("dbo.LikedSeries", "LikedRefId", "dbo.Likeds", "LikedId", cascadeDelete: true);
        }
    }
}

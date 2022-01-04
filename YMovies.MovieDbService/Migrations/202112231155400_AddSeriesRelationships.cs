namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeriesRelationships : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Watcheds", newName: "Watched");
            DropForeignKey("dbo.Movies", "Watched_Id", "dbo.Watcheds");
            DropForeignKey("dbo.Series", "Watched_Id", "dbo.Watcheds");
            DropIndex("dbo.Watched", new[] { "User_Id" });
            RenameColumn(table: "dbo.Watched", name: "User_Id", newName: "WatchedId");
            RenameColumn(table: "dbo.Movies", name: "Watched_Id", newName: "Watched_WatchedId");
            RenameColumn(table: "dbo.Series", name: "Watched_Id", newName: "Watched_WatchedId");
            RenameIndex(table: "dbo.Movies", name: "IX_Watched_Id", newName: "IX_Watched_WatchedId");
            RenameIndex(table: "dbo.Series", name: "IX_Watched_Id", newName: "IX_Watched_WatchedId");
            DropPrimaryKey("dbo.Watched");
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        SeasonId = c.Int(nullable: false),
                        Name = c.String(),
                        NumberOfEpisodes = c.Int(nullable: false),
                        StatisticId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SeasonId)
                .ForeignKey("dbo.Statistics", t => t.StatisticId, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.SeasonId)
                .Index(t => t.SeasonId)
                .Index(t => t.StatisticId);
            
            CreateTable(
                "dbo.Likeds",
                c => new
                    {
                        LikedId = c.Int(nullable: false),
                        MovieId = c.Int(nullable: false),
                        SeriesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikedId)
                .ForeignKey("dbo.Users", t => t.LikedId)
                .Index(t => t.LikedId);
            
            CreateTable(
                "dbo.SeriesCasts",
                c => new
                    {
                        Series_SeriesId = c.Int(nullable: false),
                        Cast_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Series_SeriesId, t.Cast_Id })
                .ForeignKey("dbo.Series", t => t.Series_SeriesId, cascadeDelete: true)
                .ForeignKey("dbo.Casts", t => t.Cast_Id, cascadeDelete: true)
                .Index(t => t.Series_SeriesId)
                .Index(t => t.Cast_Id);
            
            CreateTable(
                "dbo.SeriesCountries",
                c => new
                    {
                        Series_SeriesId = c.Int(nullable: false),
                        Country_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Series_SeriesId, t.Country_Id })
                .ForeignKey("dbo.Series", t => t.Series_SeriesId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .Index(t => t.Series_SeriesId)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.GenreSeries",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Series_SeriesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Series_SeriesId })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Series", t => t.Series_SeriesId, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Series_SeriesId);
            
            AddColumn("dbo.Casts", "SeriesId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "Liked_LikedId", c => c.Int());
            AddColumn("dbo.Countries", "SeriesId", c => c.Int(nullable: false));
            AddColumn("dbo.Genres", "SeriesId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "WatchedId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "LikedId", c => c.Int(nullable: false));
            AddColumn("dbo.Series", "Liked_LikedId", c => c.Int());
            AlterColumn("dbo.Watched", "WatchedId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Watched", "WatchedId");
            CreateIndex("dbo.Movies", "Liked_LikedId");
            CreateIndex("dbo.Series", "StatisticId");
            CreateIndex("dbo.Series", "Liked_LikedId");
            CreateIndex("dbo.Watched", "WatchedId");
            AddForeignKey("dbo.Series", "StatisticId", "dbo.Statistics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Movies", "Liked_LikedId", "dbo.Likeds", "LikedId");
            AddForeignKey("dbo.Series", "Liked_LikedId", "dbo.Likeds", "LikedId");
            AddForeignKey("dbo.Movies", "Watched_WatchedId", "dbo.Watched", "WatchedId");
            AddForeignKey("dbo.Series", "Watched_WatchedId", "dbo.Watched", "WatchedId");
            DropColumn("dbo.Users", "LikedFilmId");
            DropColumn("dbo.Watched", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Watched", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Users", "LikedFilmId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Series", "Watched_WatchedId", "dbo.Watched");
            DropForeignKey("dbo.Movies", "Watched_WatchedId", "dbo.Watched");
            DropForeignKey("dbo.Likeds", "LikedId", "dbo.Users");
            DropForeignKey("dbo.Series", "Liked_LikedId", "dbo.Likeds");
            DropForeignKey("dbo.Movies", "Liked_LikedId", "dbo.Likeds");
            DropForeignKey("dbo.Series", "StatisticId", "dbo.Statistics");
            DropForeignKey("dbo.Seasons", "SeasonId", "dbo.Series");
            DropForeignKey("dbo.Seasons", "StatisticId", "dbo.Statistics");
            DropForeignKey("dbo.GenreSeries", "Series_SeriesId", "dbo.Series");
            DropForeignKey("dbo.GenreSeries", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.SeriesCountries", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.SeriesCountries", "Series_SeriesId", "dbo.Series");
            DropForeignKey("dbo.SeriesCasts", "Cast_Id", "dbo.Casts");
            DropForeignKey("dbo.SeriesCasts", "Series_SeriesId", "dbo.Series");
            DropIndex("dbo.GenreSeries", new[] { "Series_SeriesId" });
            DropIndex("dbo.GenreSeries", new[] { "Genre_Id" });
            DropIndex("dbo.SeriesCountries", new[] { "Country_Id" });
            DropIndex("dbo.SeriesCountries", new[] { "Series_SeriesId" });
            DropIndex("dbo.SeriesCasts", new[] { "Cast_Id" });
            DropIndex("dbo.SeriesCasts", new[] { "Series_SeriesId" });
            DropIndex("dbo.Watched", new[] { "WatchedId" });
            DropIndex("dbo.Likeds", new[] { "LikedId" });
            DropIndex("dbo.Seasons", new[] { "StatisticId" });
            DropIndex("dbo.Seasons", new[] { "SeasonId" });
            DropIndex("dbo.Series", new[] { "Liked_LikedId" });
            DropIndex("dbo.Series", new[] { "StatisticId" });
            DropIndex("dbo.Movies", new[] { "Liked_LikedId" });
            DropPrimaryKey("dbo.Watched");
            AlterColumn("dbo.Watched", "WatchedId", c => c.Int());
            DropColumn("dbo.Series", "Liked_LikedId");
            DropColumn("dbo.Users", "LikedId");
            DropColumn("dbo.Users", "WatchedId");
            DropColumn("dbo.Genres", "SeriesId");
            DropColumn("dbo.Countries", "SeriesId");
            DropColumn("dbo.Movies", "Liked_LikedId");
            DropColumn("dbo.Casts", "SeriesId");
            DropTable("dbo.GenreSeries");
            DropTable("dbo.SeriesCountries");
            DropTable("dbo.SeriesCasts");
            DropTable("dbo.Likeds");
            DropTable("dbo.Seasons");
            AddPrimaryKey("dbo.Watched", "Id");
            RenameIndex(table: "dbo.Series", name: "IX_Watched_WatchedId", newName: "IX_Watched_Id");
            RenameIndex(table: "dbo.Movies", name: "IX_Watched_WatchedId", newName: "IX_Watched_Id");
            RenameColumn(table: "dbo.Series", name: "Watched_WatchedId", newName: "Watched_Id");
            RenameColumn(table: "dbo.Movies", name: "Watched_WatchedId", newName: "Watched_Id");
            RenameColumn(table: "dbo.Watched", name: "WatchedId", newName: "User_Id");
            CreateIndex("dbo.Watched", "User_Id");
            AddForeignKey("dbo.Series", "Watched_Id", "dbo.Watcheds", "Id");
            AddForeignKey("dbo.Movies", "Watched_Id", "dbo.Watcheds", "Id");
            RenameTable(name: "dbo.Watched", newName: "Watcheds");
        }
    }
}

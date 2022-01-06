namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Casts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        PictureUrl = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Int(nullable: false, identity: true),
                        ImdbId = c.String(),
                        Title = c.String(),
                        PosterUrl = c.String(),
                        Year = c.String(),
                        Plot = c.String(),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BoxOffice = c.String(),
                        ImdbRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfLikes = c.Int(nullable: false),
                        NumberOfDislikes = c.Int(nullable: false),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.MovieId)
                .ForeignKey("dbo.Types", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        SeriesId = c.Int(nullable: false, identity: true),
                        ImdbId = c.String(),
                        Title = c.String(),
                        PosterUrl = c.String(),
                        Year = c.String(),
                        Plot = c.String(),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BoxOffice = c.String(),
                        ImdbRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfLikes = c.Int(nullable: false),
                        NumberOfDislikes = c.Int(nullable: false),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.SeriesId)
                .ForeignKey("dbo.Types", t => t.Type_Id)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        SeasonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumberOfEpisodes = c.Int(nullable: false),
                        CurrentSeriesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SeasonId)
                .ForeignKey("dbo.Series", t => t.CurrentSeriesId, cascadeDelete: true)
                .Index(t => t.CurrentSeriesId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieCast",
                c => new
                    {
                        MovieRefId = c.Int(nullable: false),
                        CastRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieRefId, t.CastRefId })
                .ForeignKey("dbo.Movies", t => t.MovieRefId, cascadeDelete: true)
                .ForeignKey("dbo.Casts", t => t.CastRefId, cascadeDelete: true)
                .Index(t => t.MovieRefId)
                .Index(t => t.CastRefId);
            
            CreateTable(
                "dbo.SeriesCast",
                c => new
                    {
                        SeriesRefId = c.Int(nullable: false),
                        CastRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SeriesRefId, t.CastRefId })
                .ForeignKey("dbo.Series", t => t.SeriesRefId, cascadeDelete: true)
                .ForeignKey("dbo.Casts", t => t.CastRefId, cascadeDelete: true)
                .Index(t => t.SeriesRefId)
                .Index(t => t.CastRefId);
            
            CreateTable(
                "dbo.SeriesCountry",
                c => new
                    {
                        SeriesRefId = c.Int(nullable: false),
                        CountryRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SeriesRefId, t.CountryRefId })
                .ForeignKey("dbo.Series", t => t.SeriesRefId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryRefId, cascadeDelete: true)
                .Index(t => t.SeriesRefId)
                .Index(t => t.CountryRefId);
            
            CreateTable(
                "dbo.SeriesGenre",
                c => new
                    {
                        SeriesRefId = c.Int(nullable: false),
                        GenreRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SeriesRefId, t.GenreRefId })
                .ForeignKey("dbo.Series", t => t.SeriesRefId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreRefId, cascadeDelete: true)
                .Index(t => t.SeriesRefId)
                .Index(t => t.GenreRefId);
            
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
                .Index(t => t.UserRefId)
                .Index(t => t.MovieRefId);
            
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
                .Index(t => t.UserRefId)
                .Index(t => t.SeasonRefId);
            
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
                .Index(t => t.UserRefId)
                .Index(t => t.MovieRefId);
            
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
                .Index(t => t.UserRefId)
                .Index(t => t.SeasonRefId);
            
            CreateTable(
                "dbo.MovieCountry",
                c => new
                    {
                        MovieRefId = c.Int(nullable: false),
                        CountryRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieRefId, t.CountryRefId })
                .ForeignKey("dbo.Movies", t => t.MovieRefId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryRefId, cascadeDelete: true)
                .Index(t => t.MovieRefId)
                .Index(t => t.CountryRefId);
            
            CreateTable(
                "dbo.MovieGenre",
                c => new
                    {
                        MovieRefId = c.Int(nullable: false),
                        GenreRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MovieRefId, t.GenreRefId })
                .ForeignKey("dbo.Movies", t => t.MovieRefId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreRefId, cascadeDelete: true)
                .Index(t => t.MovieRefId)
                .Index(t => t.GenreRefId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "Type_Id", "dbo.Types");
            DropForeignKey("dbo.MovieGenre", "GenreRefId", "dbo.Genres");
            DropForeignKey("dbo.MovieGenre", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.MovieCountry", "CountryRefId", "dbo.Countries");
            DropForeignKey("dbo.MovieCountry", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.Series", "Type_Id", "dbo.Types");
            DropForeignKey("dbo.Seasons", "CurrentSeriesId", "dbo.Series");
            DropForeignKey("dbo.WatchedSeason", "SeasonRefId", "dbo.Seasons");
            DropForeignKey("dbo.WatchedSeason", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.WatchedMovie", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.WatchedMovie", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.LikedSeason", "SeasonRefId", "dbo.Seasons");
            DropForeignKey("dbo.LikedSeason", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.LikedMovie", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.LikedMovie", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.SeriesGenre", "GenreRefId", "dbo.Genres");
            DropForeignKey("dbo.SeriesGenre", "SeriesRefId", "dbo.Series");
            DropForeignKey("dbo.SeriesCountry", "CountryRefId", "dbo.Countries");
            DropForeignKey("dbo.SeriesCountry", "SeriesRefId", "dbo.Series");
            DropForeignKey("dbo.SeriesCast", "CastRefId", "dbo.Casts");
            DropForeignKey("dbo.SeriesCast", "SeriesRefId", "dbo.Series");
            DropForeignKey("dbo.MovieCast", "CastRefId", "dbo.Casts");
            DropForeignKey("dbo.MovieCast", "MovieRefId", "dbo.Movies");
            DropIndex("dbo.MovieGenre", new[] { "GenreRefId" });
            DropIndex("dbo.MovieGenre", new[] { "MovieRefId" });
            DropIndex("dbo.MovieCountry", new[] { "CountryRefId" });
            DropIndex("dbo.MovieCountry", new[] { "MovieRefId" });
            DropIndex("dbo.WatchedSeason", new[] { "SeasonRefId" });
            DropIndex("dbo.WatchedSeason", new[] { "UserRefId" });
            DropIndex("dbo.WatchedMovie", new[] { "MovieRefId" });
            DropIndex("dbo.WatchedMovie", new[] { "UserRefId" });
            DropIndex("dbo.LikedSeason", new[] { "SeasonRefId" });
            DropIndex("dbo.LikedSeason", new[] { "UserRefId" });
            DropIndex("dbo.LikedMovie", new[] { "MovieRefId" });
            DropIndex("dbo.LikedMovie", new[] { "UserRefId" });
            DropIndex("dbo.SeriesGenre", new[] { "GenreRefId" });
            DropIndex("dbo.SeriesGenre", new[] { "SeriesRefId" });
            DropIndex("dbo.SeriesCountry", new[] { "CountryRefId" });
            DropIndex("dbo.SeriesCountry", new[] { "SeriesRefId" });
            DropIndex("dbo.SeriesCast", new[] { "CastRefId" });
            DropIndex("dbo.SeriesCast", new[] { "SeriesRefId" });
            DropIndex("dbo.MovieCast", new[] { "CastRefId" });
            DropIndex("dbo.MovieCast", new[] { "MovieRefId" });
            DropIndex("dbo.Seasons", new[] { "CurrentSeriesId" });
            DropIndex("dbo.Series", new[] { "Type_Id" });
            DropIndex("dbo.Movies", new[] { "Type_Id" });
            DropTable("dbo.MovieGenre");
            DropTable("dbo.MovieCountry");
            DropTable("dbo.WatchedSeason");
            DropTable("dbo.WatchedMovie");
            DropTable("dbo.LikedSeason");
            DropTable("dbo.LikedMovie");
            DropTable("dbo.SeriesGenre");
            DropTable("dbo.SeriesCountry");
            DropTable("dbo.SeriesCast");
            DropTable("dbo.MovieCast");
            DropTable("dbo.Types");
            DropTable("dbo.Users");
            DropTable("dbo.Seasons");
            DropTable("dbo.Genres");
            DropTable("dbo.Series");
            DropTable("dbo.Countries");
            DropTable("dbo.Movies");
            DropTable("dbo.Casts");
        }
    }
}

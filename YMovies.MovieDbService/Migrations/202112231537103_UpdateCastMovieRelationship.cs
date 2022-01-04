namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCastMovieRelationship : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.MovieCasts", newName: "MovieCast");
            DropForeignKey("dbo.CountryMovies", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.CountryMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.SeriesCasts", "Series_SeriesId", "dbo.Series");
            DropForeignKey("dbo.SeriesCasts", "Cast_Id", "dbo.Casts");
            DropForeignKey("dbo.SeriesCountries", "Series_SeriesId", "dbo.Series");
            DropForeignKey("dbo.SeriesCountries", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.GenreMovies", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.GenreMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.GenreSeries", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.GenreSeries", "Series_SeriesId", "dbo.Series");
            DropForeignKey("dbo.Seasons", "StatisticId", "dbo.Statistics");
            DropForeignKey("dbo.Seasons", "SeasonId", "dbo.Series");
            DropForeignKey("dbo.Series", "StatisticId", "dbo.Statistics");
            DropForeignKey("dbo.Movies", "StatisticId", "dbo.Statistics");
            DropForeignKey("dbo.Movies", "Liked_LikedId", "dbo.Likeds");
            DropForeignKey("dbo.Series", "Liked_LikedId", "dbo.Likeds");
            DropForeignKey("dbo.Likeds", "LikedId", "dbo.Users");
            DropForeignKey("dbo.Movies", "Watched_WatchedId", "dbo.Watched");
            DropForeignKey("dbo.Series", "Watched_WatchedId", "dbo.Watched");
            DropForeignKey("dbo.Watched", "WatchedId", "dbo.Users");
            DropIndex("dbo.Movies", new[] { "StatisticId" });
            DropIndex("dbo.Movies", new[] { "Liked_LikedId" });
            DropIndex("dbo.Movies", new[] { "Watched_WatchedId" });
            DropIndex("dbo.Series", new[] { "StatisticId" });
            DropIndex("dbo.Series", new[] { "Liked_LikedId" });
            DropIndex("dbo.Series", new[] { "Watched_WatchedId" });
            DropIndex("dbo.Seasons", new[] { "SeasonId" });
            DropIndex("dbo.Seasons", new[] { "StatisticId" });
            DropIndex("dbo.Likeds", new[] { "LikedId" });
            DropIndex("dbo.Watched", new[] { "WatchedId" });
            DropIndex("dbo.CountryMovies", new[] { "Country_Id" });
            DropIndex("dbo.CountryMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.SeriesCasts", new[] { "Series_SeriesId" });
            DropIndex("dbo.SeriesCasts", new[] { "Cast_Id" });
            DropIndex("dbo.SeriesCountries", new[] { "Series_SeriesId" });
            DropIndex("dbo.SeriesCountries", new[] { "Country_Id" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_Id" });
            DropIndex("dbo.GenreMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.GenreSeries", new[] { "Genre_Id" });
            DropIndex("dbo.GenreSeries", new[] { "Series_SeriesId" });
            RenameColumn(table: "dbo.MovieCast", name: "Movie_MovieId", newName: "MovieRefId");
            RenameColumn(table: "dbo.MovieCast", name: "Cast_Id", newName: "CastRefId");
            RenameIndex(table: "dbo.MovieCast", name: "IX_Movie_MovieId", newName: "IX_MovieRefId");
            RenameIndex(table: "dbo.MovieCast", name: "IX_Cast_Id", newName: "IX_CastRefId");
            AddColumn("dbo.Casts", "PictureUrl", c => c.String());
            DropColumn("dbo.Movies", "CountryId");
            DropColumn("dbo.Movies", "CastId");
            DropColumn("dbo.Movies", "GenreId");
            DropColumn("dbo.Movies", "StatisticId");
            DropColumn("dbo.Movies", "Liked_LikedId");
            DropColumn("dbo.Movies", "Watched_WatchedId");
            DropColumn("dbo.Users", "WatchedId");
            DropColumn("dbo.Users", "LikedId");
            DropTable("dbo.Series");
            DropTable("dbo.Seasons");
            DropTable("dbo.Likeds");
            DropTable("dbo.Watched");
            DropTable("dbo.CountryMovies");
            DropTable("dbo.SeriesCasts");
            DropTable("dbo.SeriesCountries");
            DropTable("dbo.GenreMovies");
            DropTable("dbo.GenreSeries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GenreSeries",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Series_SeriesId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Series_SeriesId });
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Movie_MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Movie_MovieId });
            
            CreateTable(
                "dbo.SeriesCountries",
                c => new
                    {
                        Series_SeriesId = c.Int(nullable: false),
                        Country_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Series_SeriesId, t.Country_Id });
            
            CreateTable(
                "dbo.SeriesCasts",
                c => new
                    {
                        Series_SeriesId = c.Int(nullable: false),
                        Cast_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Series_SeriesId, t.Cast_Id });
            
            CreateTable(
                "dbo.CountryMovies",
                c => new
                    {
                        Country_Id = c.Int(nullable: false),
                        Movie_MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Country_Id, t.Movie_MovieId });
            
            CreateTable(
                "dbo.Watched",
                c => new
                    {
                        WatchedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WatchedId);
            
            CreateTable(
                "dbo.Likeds",
                c => new
                    {
                        LikedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LikedId);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        SeasonId = c.Int(nullable: false),
                        Name = c.String(),
                        NumberOfEpisodes = c.Int(nullable: false),
                        StatisticId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SeasonId);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        SeriesId = c.Int(nullable: false, identity: true),
                        SeasonId = c.Int(nullable: false),
                        ImdbId = c.String(),
                        Title = c.String(),
                        PosterUrl = c.String(),
                        Year = c.String(),
                        CountryId = c.Int(nullable: false),
                        CastId = c.Int(nullable: false),
                        Plot = c.String(),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BoxOffice = c.String(),
                        ImdbRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StatisticId = c.Int(nullable: false),
                        Liked_LikedId = c.Int(),
                        Watched_WatchedId = c.Int(),
                    })
                .PrimaryKey(t => t.SeriesId);
            
            AddColumn("dbo.Users", "LikedId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "WatchedId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "Watched_WatchedId", c => c.Int());
            AddColumn("dbo.Movies", "Liked_LikedId", c => c.Int());
            AddColumn("dbo.Movies", "StatisticId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "GenreId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "CastId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "CountryId", c => c.Int(nullable: false));
            DropColumn("dbo.Casts", "PictureUrl");
            RenameIndex(table: "dbo.MovieCast", name: "IX_CastRefId", newName: "IX_Cast_Id");
            RenameIndex(table: "dbo.MovieCast", name: "IX_MovieRefId", newName: "IX_Movie_MovieId");
            RenameColumn(table: "dbo.MovieCast", name: "CastRefId", newName: "Cast_Id");
            RenameColumn(table: "dbo.MovieCast", name: "MovieRefId", newName: "Movie_MovieId");
            CreateIndex("dbo.GenreSeries", "Series_SeriesId");
            CreateIndex("dbo.GenreSeries", "Genre_Id");
            CreateIndex("dbo.GenreMovies", "Movie_MovieId");
            CreateIndex("dbo.GenreMovies", "Genre_Id");
            CreateIndex("dbo.SeriesCountries", "Country_Id");
            CreateIndex("dbo.SeriesCountries", "Series_SeriesId");
            CreateIndex("dbo.SeriesCasts", "Cast_Id");
            CreateIndex("dbo.SeriesCasts", "Series_SeriesId");
            CreateIndex("dbo.CountryMovies", "Movie_MovieId");
            CreateIndex("dbo.CountryMovies", "Country_Id");
            CreateIndex("dbo.Watched", "WatchedId");
            CreateIndex("dbo.Likeds", "LikedId");
            CreateIndex("dbo.Seasons", "StatisticId");
            CreateIndex("dbo.Seasons", "SeasonId");
            CreateIndex("dbo.Series", "Watched_WatchedId");
            CreateIndex("dbo.Series", "Liked_LikedId");
            CreateIndex("dbo.Series", "StatisticId");
            CreateIndex("dbo.Movies", "Watched_WatchedId");
            CreateIndex("dbo.Movies", "Liked_LikedId");
            CreateIndex("dbo.Movies", "StatisticId");
            AddForeignKey("dbo.Watched", "WatchedId", "dbo.Users", "Id");
            AddForeignKey("dbo.Series", "Watched_WatchedId", "dbo.Watched", "WatchedId");
            AddForeignKey("dbo.Movies", "Watched_WatchedId", "dbo.Watched", "WatchedId");
            AddForeignKey("dbo.Likeds", "LikedId", "dbo.Users", "Id");
            AddForeignKey("dbo.Series", "Liked_LikedId", "dbo.Likeds", "LikedId");
            AddForeignKey("dbo.Movies", "Liked_LikedId", "dbo.Likeds", "LikedId");
            AddForeignKey("dbo.Movies", "StatisticId", "dbo.Statistics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Series", "StatisticId", "dbo.Statistics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Seasons", "SeasonId", "dbo.Series", "SeriesId");
            AddForeignKey("dbo.Seasons", "StatisticId", "dbo.Statistics", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreSeries", "Series_SeriesId", "dbo.Series", "SeriesId", cascadeDelete: true);
            AddForeignKey("dbo.GenreSeries", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreMovies", "Movie_MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
            AddForeignKey("dbo.GenreMovies", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SeriesCountries", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SeriesCountries", "Series_SeriesId", "dbo.Series", "SeriesId", cascadeDelete: true);
            AddForeignKey("dbo.SeriesCasts", "Cast_Id", "dbo.Casts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SeriesCasts", "Series_SeriesId", "dbo.Series", "SeriesId", cascadeDelete: true);
            AddForeignKey("dbo.CountryMovies", "Movie_MovieId", "dbo.Movies", "MovieId", cascadeDelete: true);
            AddForeignKey("dbo.CountryMovies", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.MovieCast", newName: "MovieCasts");
        }
    }
}

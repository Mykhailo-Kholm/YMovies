namespace YMovies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModelOfMovies : DbMigration
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
                        MovieId = c.Int(nullable: false),
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
                        CountryId = c.Int(nullable: false),
                        CastId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                        Plot = c.String(),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BoxOffice = c.String(),
                        ImdbRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StatisticId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovieId)
                .ForeignKey("dbo.Statistics", t => t.StatisticId, cascadeDelete: true)
                .Index(t => t.StatisticId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfLikes = c.Int(nullable: false),
                        NumberOfDislikes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MovieCasts",
                c => new
                    {
                        Movie_MovieId = c.Int(nullable: false),
                        Cast_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_MovieId, t.Cast_Id })
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .ForeignKey("dbo.Casts", t => t.Cast_Id, cascadeDelete: true)
                .Index(t => t.Movie_MovieId)
                .Index(t => t.Cast_Id);
            
            CreateTable(
                "dbo.CountryMovies",
                c => new
                    {
                        Country_Id = c.Int(nullable: false),
                        Movie_MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Country_Id, t.Movie_MovieId })
                .ForeignKey("dbo.Countries", t => t.Country_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .Index(t => t.Country_Id)
                .Index(t => t.Movie_MovieId);
            
            CreateTable(
                "dbo.GenreMovies",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Movie_MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Movie_MovieId })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_MovieId, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Movie_MovieId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "StatisticId", "dbo.Statistics");
            DropForeignKey("dbo.GenreMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.GenreMovies", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.CountryMovies", "Movie_MovieId", "dbo.Movies");
            DropForeignKey("dbo.CountryMovies", "Country_Id", "dbo.Countries");
            DropForeignKey("dbo.MovieCasts", "Cast_Id", "dbo.Casts");
            DropForeignKey("dbo.MovieCasts", "Movie_MovieId", "dbo.Movies");
            DropIndex("dbo.GenreMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.GenreMovies", new[] { "Genre_Id" });
            DropIndex("dbo.CountryMovies", new[] { "Movie_MovieId" });
            DropIndex("dbo.CountryMovies", new[] { "Country_Id" });
            DropIndex("dbo.MovieCasts", new[] { "Cast_Id" });
            DropIndex("dbo.MovieCasts", new[] { "Movie_MovieId" });
            DropIndex("dbo.Movies", new[] { "StatisticId" });
            DropTable("dbo.GenreMovies");
            DropTable("dbo.CountryMovies");
            DropTable("dbo.MovieCasts");
            DropTable("dbo.Statistics");
            DropTable("dbo.Genres");
            DropTable("dbo.Countries");
            DropTable("dbo.Movies");
            DropTable("dbo.Casts");
        }
    }
}

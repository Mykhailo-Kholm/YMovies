namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCountryMovieAndGenreMovieRelationship : DbMigration
    {
        public override void Up()
        {
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
            DropForeignKey("dbo.MovieGenre", "GenreRefId", "dbo.Genres");
            DropForeignKey("dbo.MovieGenre", "MovieRefId", "dbo.Movies");
            DropForeignKey("dbo.MovieCountry", "CountryRefId", "dbo.Countries");
            DropForeignKey("dbo.MovieCountry", "MovieRefId", "dbo.Movies");
            DropIndex("dbo.MovieGenre", new[] { "GenreRefId" });
            DropIndex("dbo.MovieGenre", new[] { "MovieRefId" });
            DropIndex("dbo.MovieCountry", new[] { "CountryRefId" });
            DropIndex("dbo.MovieCountry", new[] { "MovieRefId" });
            DropTable("dbo.MovieGenre");
            DropTable("dbo.MovieCountry");
        }
    }
}

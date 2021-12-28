namespace YMovies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCountryCastGenreAndSeriesRelationship : DbMigration
    {
        public override void Up()
        {
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
                        StatisticId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SeriesId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeriesGenre", "GenreRefId", "dbo.Genres");
            DropForeignKey("dbo.SeriesGenre", "SeriesRefId", "dbo.Series");
            DropForeignKey("dbo.SeriesCountry", "CountryRefId", "dbo.Countries");
            DropForeignKey("dbo.SeriesCountry", "SeriesRefId", "dbo.Series");
            DropForeignKey("dbo.SeriesCast", "CastRefId", "dbo.Casts");
            DropForeignKey("dbo.SeriesCast", "SeriesRefId", "dbo.Series");
            DropIndex("dbo.SeriesGenre", new[] { "GenreRefId" });
            DropIndex("dbo.SeriesGenre", new[] { "SeriesRefId" });
            DropIndex("dbo.SeriesCountry", new[] { "CountryRefId" });
            DropIndex("dbo.SeriesCountry", new[] { "SeriesRefId" });
            DropIndex("dbo.SeriesCast", new[] { "CastRefId" });
            DropIndex("dbo.SeriesCast", new[] { "SeriesRefId" });
            DropTable("dbo.SeriesGenre");
            DropTable("dbo.SeriesCountry");
            DropTable("dbo.SeriesCast");
            DropTable("dbo.Series");
        }
    }
}

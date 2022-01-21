namespace YMovies.MovieDbService.Migrations
{
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
                "dbo.Media",
                c => new
                    {
                        MediaId = c.Int(nullable: false, identity: true),
                        ImdbId = c.String(),
                        Title = c.String(),
                        PosterUrl = c.String(),
                        Year = c.String(),
                        Plot = c.String(),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Companies = c.String(),
                        ImdbRating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NumberOfLikes = c.Int(nullable: false),
                        NumberOfDislikes = c.Int(nullable: false),
                        Type_Id = c.Int(),
                    })
                .PrimaryKey(t => t.MediaId)
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
                .ForeignKey("dbo.Media", t => t.CurrentSeriesId, cascadeDelete: true)
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
                "dbo.MediaCast",
                c => new
                    {
                        MediaRefId = c.Int(nullable: false),
                        CastRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MediaRefId, t.CastRefId })
                .ForeignKey("dbo.Media", t => t.MediaRefId, cascadeDelete: true)
                .ForeignKey("dbo.Casts", t => t.CastRefId, cascadeDelete: true)
                .Index(t => t.MediaRefId)
                .Index(t => t.CastRefId);
            
            CreateTable(
                "dbo.MediaCountry",
                c => new
                    {
                        MediaRefId = c.Int(nullable: false),
                        CountryRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MediaRefId, t.CountryRefId })
                .ForeignKey("dbo.Media", t => t.MediaRefId, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryRefId, cascadeDelete: true)
                .Index(t => t.MediaRefId)
                .Index(t => t.CountryRefId);
            
            CreateTable(
                "dbo.MediaGenre",
                c => new
                    {
                        MediaRefId = c.Int(nullable: false),
                        GenreRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MediaRefId, t.GenreRefId })
                .ForeignKey("dbo.Media", t => t.MediaRefId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreRefId, cascadeDelete: true)
                .Index(t => t.MediaRefId)
                .Index(t => t.GenreRefId);
            
            CreateTable(
                "dbo.LikedMedia",
                c => new
                    {
                        UserRefId = c.Int(nullable: false),
                        MediaRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.MediaRefId })
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.MediaRefId, cascadeDelete: true)
                .Index(t => t.UserRefId)
                .Index(t => t.MediaRefId);
            
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
                "dbo.WatchedMedia",
                c => new
                    {
                        UserRefId = c.Int(nullable: false),
                        MediaRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserRefId, t.MediaRefId })
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.MediaRefId, cascadeDelete: true)
                .Index(t => t.UserRefId)
                .Index(t => t.MediaRefId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Media", "Type_Id", "dbo.Types");
            DropForeignKey("dbo.Seasons", "CurrentSeriesId", "dbo.Media");
            DropForeignKey("dbo.WatchedSeason", "SeasonRefId", "dbo.Seasons");
            DropForeignKey("dbo.WatchedSeason", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.WatchedMedia", "MediaRefId", "dbo.Media");
            DropForeignKey("dbo.WatchedMedia", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.LikedSeason", "SeasonRefId", "dbo.Seasons");
            DropForeignKey("dbo.LikedSeason", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.LikedMedia", "MediaRefId", "dbo.Media");
            DropForeignKey("dbo.LikedMedia", "UserRefId", "dbo.Users");
            DropForeignKey("dbo.MediaGenre", "GenreRefId", "dbo.Genres");
            DropForeignKey("dbo.MediaGenre", "MediaRefId", "dbo.Media");
            DropForeignKey("dbo.MediaCountry", "CountryRefId", "dbo.Countries");
            DropForeignKey("dbo.MediaCountry", "MediaRefId", "dbo.Media");
            DropForeignKey("dbo.MediaCast", "CastRefId", "dbo.Casts");
            DropForeignKey("dbo.MediaCast", "MediaRefId", "dbo.Media");
            DropIndex("dbo.WatchedSeason", new[] { "SeasonRefId" });
            DropIndex("dbo.WatchedSeason", new[] { "UserRefId" });
            DropIndex("dbo.WatchedMedia", new[] { "MediaRefId" });
            DropIndex("dbo.WatchedMedia", new[] { "UserRefId" });
            DropIndex("dbo.LikedSeason", new[] { "SeasonRefId" });
            DropIndex("dbo.LikedSeason", new[] { "UserRefId" });
            DropIndex("dbo.LikedMedia", new[] { "MediaRefId" });
            DropIndex("dbo.LikedMedia", new[] { "UserRefId" });
            DropIndex("dbo.MediaGenre", new[] { "GenreRefId" });
            DropIndex("dbo.MediaGenre", new[] { "MediaRefId" });
            DropIndex("dbo.MediaCountry", new[] { "CountryRefId" });
            DropIndex("dbo.MediaCountry", new[] { "MediaRefId" });
            DropIndex("dbo.MediaCast", new[] { "CastRefId" });
            DropIndex("dbo.MediaCast", new[] { "MediaRefId" });
            DropIndex("dbo.Seasons", new[] { "CurrentSeriesId" });
            DropIndex("dbo.Media", new[] { "Type_Id" });
            DropTable("dbo.WatchedSeason");
            DropTable("dbo.WatchedMedia");
            DropTable("dbo.LikedSeason");
            DropTable("dbo.LikedMedia");
            DropTable("dbo.MediaGenre");
            DropTable("dbo.MediaCountry");
            DropTable("dbo.MediaCast");
            DropTable("dbo.Types");
            DropTable("dbo.Users");
            DropTable("dbo.Seasons");
            DropTable("dbo.Genres");
            DropTable("dbo.Countries");
            DropTable("dbo.Media");
            DropTable("dbo.Casts");
        }
    }
}

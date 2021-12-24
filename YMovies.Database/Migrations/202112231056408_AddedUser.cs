namespace YMovies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LikedFilmId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Watcheds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        SeriesId = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
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
                        Watched_Id = c.Int(),
                    })
                .PrimaryKey(t => t.SeriesId)
                .ForeignKey("dbo.Watcheds", t => t.Watched_Id)
                .Index(t => t.Watched_Id);
            
            AddColumn("dbo.Movies", "Watched_Id", c => c.Int());
            CreateIndex("dbo.Movies", "Watched_Id");
            AddForeignKey("dbo.Movies", "Watched_Id", "dbo.Watcheds", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Watcheds", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Series", "Watched_Id", "dbo.Watcheds");
            DropForeignKey("dbo.Movies", "Watched_Id", "dbo.Watcheds");
            DropIndex("dbo.Series", new[] { "Watched_Id" });
            DropIndex("dbo.Watcheds", new[] { "User_Id" });
            DropIndex("dbo.Movies", new[] { "Watched_Id" });
            DropColumn("dbo.Movies", "Watched_Id");
            DropTable("dbo.Series");
            DropTable("dbo.Watcheds");
            DropTable("dbo.Users");
        }
    }
}

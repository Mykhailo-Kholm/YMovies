namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateModles : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Casts", "MovieId");
            DropColumn("dbo.Casts", "SeriesId");
            DropColumn("dbo.Countries", "MovieId");
            DropColumn("dbo.Countries", "SeriesId");
            DropColumn("dbo.Genres", "MovieId");
            DropColumn("dbo.Genres", "SeriesId");
            DropColumn("dbo.Likeds", "MovieId");
            DropColumn("dbo.Likeds", "SeriesId");
            DropColumn("dbo.Watched", "MovieId");
            DropColumn("dbo.Watched", "SeriesId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Watched", "SeriesId", c => c.Int(nullable: false));
            AddColumn("dbo.Watched", "MovieId", c => c.Int(nullable: false));
            AddColumn("dbo.Likeds", "SeriesId", c => c.Int(nullable: false));
            AddColumn("dbo.Likeds", "MovieId", c => c.Int(nullable: false));
            AddColumn("dbo.Genres", "SeriesId", c => c.Int(nullable: false));
            AddColumn("dbo.Genres", "MovieId", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "SeriesId", c => c.Int(nullable: false));
            AddColumn("dbo.Countries", "MovieId", c => c.Int(nullable: false));
            AddColumn("dbo.Casts", "SeriesId", c => c.Int(nullable: false));
            AddColumn("dbo.Casts", "MovieId", c => c.Int(nullable: false));
        }
    }
}

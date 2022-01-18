namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTrailerUrlToMedia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "TrailerUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Media", "TrailerUrl");
        }
    }
}

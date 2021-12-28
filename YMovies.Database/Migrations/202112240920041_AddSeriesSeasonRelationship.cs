namespace YMovies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeriesSeasonRelationship : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seasons", "CurrentSeriesId", c => c.Int(nullable: false));
            CreateIndex("dbo.Seasons", "CurrentSeriesId");
            AddForeignKey("dbo.Seasons", "CurrentSeriesId", "dbo.Series", "SeriesId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seasons", "CurrentSeriesId", "dbo.Series");
            DropIndex("dbo.Seasons", new[] { "CurrentSeriesId" });
            DropColumn("dbo.Seasons", "CurrentSeriesId");
        }
    }
}

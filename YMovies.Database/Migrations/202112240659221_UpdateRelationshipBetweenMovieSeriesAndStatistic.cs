namespace YMovies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRelationshipBetweenMovieSeriesAndStatistic : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Statistics");
            AddColumn("dbo.Statistics", "StatisticId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Statistics", "StatisticId");
            CreateIndex("dbo.Statistics", "StatisticId");
            AddForeignKey("dbo.Statistics", "StatisticId", "dbo.Series", "SeriesId");
            AddForeignKey("dbo.Statistics", "StatisticId", "dbo.Movies", "MovieId");
            DropColumn("dbo.Statistics", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Statistics", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Statistics", "StatisticId", "dbo.Movies");
            DropForeignKey("dbo.Statistics", "StatisticId", "dbo.Series");
            DropIndex("dbo.Statistics", new[] { "StatisticId" });
            DropPrimaryKey("dbo.Statistics");
            DropColumn("dbo.Statistics", "StatisticId");
            AddPrimaryKey("dbo.Statistics", "Id");
        }
    }
}

namespace YMovies.MovieDbService.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ReInit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Media", "GlobalFees", c => c.String());
            AddColumn("dbo.Media", "WeekFees", c => c.String());
            AddColumn("dbo.Seasons", "NumberOfLikes", c => c.Int(nullable: false));
            AddColumn("dbo.Seasons", "NumberOfDislikes", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Seasons", "NumberOfDislikes");
            DropColumn("dbo.Seasons", "NumberOfLikes");
            DropColumn("dbo.Media", "WeekFees");
            DropColumn("dbo.Media", "GlobalFees");
        }
    }
}

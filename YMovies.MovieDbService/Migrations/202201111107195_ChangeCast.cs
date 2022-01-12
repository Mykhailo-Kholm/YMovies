namespace YMovies.MovieDbService.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeCast : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Casts", "Surname");
            DropColumn("dbo.Casts", "PictureUrl");
        }

        public override void Down()
        {
            AddColumn("dbo.Casts", "PictureUrl", c => c.String());
            AddColumn("dbo.Casts", "Surname", c => c.String());
        }
    }
}

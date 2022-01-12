namespace YMovies.MovieDbService.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeIdTypeUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IdentityId", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Users", "IdentityId");
        }
    }
}

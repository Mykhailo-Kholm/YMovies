namespace YMovies.Identity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddNewField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "YourMumName", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "YourMumName");
        }
    }
}

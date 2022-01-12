namespace YMovies.Identity.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ChangeUserFiledName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SecondName", c => c.String());
            DropColumn("dbo.AspNetUsers", "Surname");
        }

        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Surname", c => c.String());
            DropColumn("dbo.AspNetUsers", "SecondName");
        }
    }
}

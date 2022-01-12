namespace YMovies.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUselessField : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "YourMumName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "YourMumName", c => c.String());
        }
    }
}

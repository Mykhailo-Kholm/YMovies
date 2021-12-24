namespace YMovies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        SeasonId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumberOfEpisodes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SeasonId);
            
            AddColumn("dbo.Users", "FullName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "FullName");
            DropTable("dbo.Seasons");
        }
    }
}

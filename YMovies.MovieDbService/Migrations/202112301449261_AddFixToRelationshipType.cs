namespace YMovies.MovieDbService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFixToRelationshipType : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Movies", "TypeId");
            DropColumn("dbo.Series", "TypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Series", "TypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "TypeId", c => c.Int(nullable: false));
        }
    }
}

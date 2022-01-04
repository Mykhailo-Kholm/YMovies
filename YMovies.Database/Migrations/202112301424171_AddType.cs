namespace YMovies.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Types",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Movies", "TypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Series", "TypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Types", "Id", "dbo.Movies");
            DropForeignKey("dbo.Types", "Id", "dbo.Series");
            DropIndex("dbo.Types", new[] { "Id" });
            DropColumn("dbo.Series", "TypeId");
            DropColumn("dbo.Movies", "TypeId");
            DropTable("dbo.Types");
        }
    }
}

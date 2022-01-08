namespace YMovies.Identity.Migrations
{
    using System.Data.Entity.Migrations;
    internal sealed class Configuration : DbMigrationsConfiguration<YMovies.Identity.IdentityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "YMovies.Identity.IdentityContext";
        }

    }
}

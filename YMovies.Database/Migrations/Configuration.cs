using System.Data.Entity.Migrations;
using YMovies.Database.DatabaseContext;
using YMovies.Database.Models;

namespace YMovies.Database.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MoviesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MoviesContext context)
        {

        }
    }
}

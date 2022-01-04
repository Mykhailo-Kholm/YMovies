using System.Data.Entity.Migrations;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;

namespace YMovies.MovieDbService.Migrations
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

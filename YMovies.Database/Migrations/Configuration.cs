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
            context.Movies.AddOrUpdate(m => m.MovieId,
                new Movie(){BoxOffice = "MCU",Budget = 300000, ImdbId = "tt3241334",ImdbRating = 8.7m, Plot = "Poduvus pobachush",PosterUrl = "#",Title = "YourMum", Year = "1994" },
            new Movie() { BoxOffice = "MCU", Budget = 300000, ImdbId = "tt3241334", ImdbRating = 8.7m, Plot = "Poduvus pobachush", PosterUrl = "#", Title = "YourMum2", Year = "1995" },
            new Movie() { BoxOffice = "MCU", Budget = 300000, ImdbId = "tt3241334", ImdbRating = 8.7m, Plot = "Poduvus pobachush", PosterUrl = "#", Title = "YourMum2", Year = "1995" });
            context.Countries.AddOrUpdate();
        }
    }
}

using System.Collections.Generic;
using YMovies.Database.Models;

namespace YMovies.Database.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<YMovies.Database.MoviesContext.MoviesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(YMovies.Database.MoviesContext.MoviesContext context)
        {
            context.Movies.AddOrUpdate(m => m.MovieId,
                new Movie(){BoxOffice = "MCU",Budget = 300000, ImdbId = "tt3241334",ImdbRating = 8.7m, Plot = "Poduvus pobachush",PosterUrl = "#",Title = "YourMum", Year = "1994" },
            new Movie() { BoxOffice = "MCU", Budget = 300000, ImdbId = "tt3241334", ImdbRating = 8.7m, Plot = "Poduvus pobachush", PosterUrl = "#", Title = "YourMum2", Year = "1995" },
            new Movie() { BoxOffice = "MCU", Budget = 300000, ImdbId = "tt3241334", ImdbRating = 8.7m, Plot = "Poduvus pobachush", PosterUrl = "#", Title = "YourMum2", Year = "1995" });
            context.Countries.AddOrUpdate();
        }
    }
}

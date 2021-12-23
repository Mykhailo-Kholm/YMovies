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
        }
    }
}

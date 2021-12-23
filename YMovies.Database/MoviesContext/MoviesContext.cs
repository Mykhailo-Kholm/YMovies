using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YMovies.Database.Models;

namespace YMovies.Database.MoviesContext
{
    public class MoviesContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Statistic> Statistics { get; set; }

        public MoviesContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasMany<Cast>(m => m.Cast)
                .WithMany(c => c.Movies)
                .Map(mc =>
                {
                    mc.MapLeftKey("MovieRefId");
                    mc.MapRightKey("CastRefId");
                    mc.ToTable("MovieCast");
                });
            modelBuilder.Entity<Movie>()
                .HasMany<Country>(m => m.Countries)
                .WithMany(c => c.Movies)
                .Map(mc =>
                {
                    mc.MapLeftKey("MovieRefId");
                    mc.MapRightKey("CountryRefId");
                    mc.ToTable("MovieCountry");
                });
            modelBuilder.Entity<Movie>()
                .HasMany<Genre>(m => m.Genres)
                .WithMany(c => c.Movies)
                .Map(mc =>
                {
                    mc.MapLeftKey("MovieRefId");
                    mc.MapRightKey("GenreRefId");
                    mc.ToTable("MovieGenre");
                });
            modelBuilder.Entity<Series>()
                .HasMany<Cast>(m => m.Cast)
                .WithMany(c => c.Series)
                .Map(mc =>
                {
                    mc.MapLeftKey("SeriesRefId");
                    mc.MapRightKey("CastRefId");
                    mc.ToTable("SeriesCast");
                });
            modelBuilder.Entity<Series>()
                .HasMany<Country>(m => m.Countries)
                .WithMany(c => c.Series)
                .Map(mc =>
                {
                    mc.MapLeftKey("SeriesRefId");
                    mc.MapRightKey("CountryRefId");
                    mc.ToTable("SeriesCountry");
                });
            modelBuilder.Entity<Series>()
                .HasMany<Genre>(m => m.Genres)
                .WithMany(c => c.Series)
                .Map(mc =>
                {
                    mc.MapLeftKey("SeriesRefId");
                    mc.MapRightKey("GenreRefId");
                    mc.ToTable("SeriesGenre");
                });
        }
    }
}

using System.Data.Entity;
using YMovies.MovieDbService.Models;

namespace YMovies.MovieDbService.DatabaseContext
{
    public class MoviesContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public MoviesContext() : base("name=MoviesDb")
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
            modelBuilder.Entity<Series>()
                .HasMany<Season>(sr => sr.Seasons)
                .WithRequired(s => s.CurrentSeries)
                .HasForeignKey<int>(s => s.CurrentSeriesId);
            modelBuilder.Entity<Movie>()
                .HasOptional(m => m.Statistic)
                .WithRequired(st => st.Movie);
            modelBuilder.Entity<Series>()
                .HasOptional(s => s.Statistic)
                .WithRequired(st => st.Series);

            modelBuilder.Entity<User>()
                .HasMany<Movie>(u => u.LikedMovies)
                .WithMany(m => m.UsersLiked)
                .Map(mu =>
                {
                    mu.MapLeftKey("UserRefId");
                    mu.MapRightKey("MovieRefId");
                    mu.ToTable("LikedMovie");
                });
            modelBuilder.Entity<User>()
                .HasMany<Season>(u => u.LikedSeasons)
                .WithMany(s => s.UsersLiked)
                .Map(su =>
                {
                    su.MapLeftKey("UserRefId");
                    su.MapRightKey("SeasonRefId");
                    su.ToTable("LikedSeason");
                });
            modelBuilder.Entity<User>()
                .HasMany<Movie>(u => u.WatchedMovies)
                .WithMany(m => m.UsersWatched)
                .Map(mu =>
                {
                    mu.MapLeftKey("UserRefId");
                    mu.MapRightKey("MovieRefId");
                    mu.ToTable("WatchedMovie");
                });
            modelBuilder.Entity<User>()
                .HasMany<Season>(u => u.WatchedSeasons)
                .WithMany(s => s.UsersWatched)
                .Map(su =>
                {
                    su.MapLeftKey("UserRefId");
                    su.MapRightKey("SeasonRefId");
                    su.ToTable("WatchedSeason");
                });
            modelBuilder.Entity<Movie>()
                .HasRequired(m => m.Type)
                .WithRequiredPrincipal(t => t.Movie);
            modelBuilder.Entity<Series>()
                .HasRequired(s => s.Type)
                .WithRequiredPrincipal(t => t.Series);

        }
    }
}

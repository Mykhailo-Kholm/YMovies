using System.Data.Entity;
using YMovies.Database.Models;

namespace YMovies.Database.DatabaseContext
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
                .HasRequired(u => u.Liked)
                .WithRequiredPrincipal(l => l.User);
            modelBuilder.Entity<User>()
                .HasRequired(u => u.Watched)
                .WithRequiredPrincipal(w => w.User);
            modelBuilder.Entity<Liked>()
                .HasMany<Movie>(l => l.LikedMovies)
                .WithMany(m => m.Liked)
                .Map(ml =>
                {
                    ml.MapLeftKey("LikedRefId");
                    ml.MapRightKey("MovieRefId");
                    ml.ToTable("LikedMovie");
                });
            modelBuilder.Entity<Liked>()
                .HasMany<Season>(l => l.LikedSeasons)
                .WithMany(s => s.Liked)
                .Map(sl =>
                {
                    sl.MapLeftKey("LikedRefId");
                    sl.MapRightKey("SeasonRefId");
                    sl.ToTable("LikedSeason");
                });
            modelBuilder.Entity<Watched>()
                .HasMany<Movie>(w => w.WatchedMovies)
                .WithMany(m => m.Watched)
                .Map(mw =>
                {
                    mw.MapLeftKey("WatchedRefId");
                    mw.MapRightKey("MovieRefId");
                    mw.ToTable("WatchedMovie");
                });
            modelBuilder.Entity<Watched>()
                .HasMany<Season>(w => w.WatchedSeasons)
                .WithMany(s => s.Watched)
                .Map(wl =>
                {
                    wl.MapLeftKey("WatchedRefId");
                    wl.MapRightKey("SeasonRefId");
                    wl.ToTable("WatchedSeason");
                });

        }
    }
}

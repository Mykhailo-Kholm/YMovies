using System.Data.Entity;
using YMovies.MovieDbService.Models;

namespace YMovies.MovieDbService.DatabaseContext
{
    public class MoviesContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Cast> Cast { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Type> Types { get; set; }
        public MoviesContext() : base("name=DefaultConnection")
        {
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Media>()
                .HasMany<Cast>(m => m.Cast)
                .WithMany(c => c.Medias)
                .Map(mc =>
                {
                    mc.MapLeftKey("MediaRefId");
                    mc.MapRightKey("CastRefId");
                    mc.ToTable("MediaCast");
                });
            modelBuilder.Entity<Media>()
                .HasMany<Country>(m => m.Countries)
                .WithMany(c => c.Medias)
                .Map(mc =>
                {
                    mc.MapLeftKey("MediaRefId");
                    mc.MapRightKey("CountryRefId");
                    mc.ToTable("MediaCountry");
                });
            modelBuilder.Entity<Media>()
                .HasMany<Genre>(m => m.Genres)
                .WithMany(c => c.Medias)
                .Map(mc =>
                {
                    mc.MapLeftKey("MediaRefId");
                    mc.MapRightKey("GenreRefId");
                    mc.ToTable("MediaGenre");
                });
            //modelBuilder.Entity<Media>()
            //    .HasMany<Cast>(m => m.Cast)
            //    .WithMany(c => c.Medias)
            //    .Map(mc =>
            //    {
            //        mc.MapLeftKey("SeriesRefId");
            //        mc.MapRightKey("CastRefId");
            //        mc.ToTable("SeriesCast");
            //    });
            //modelBuilder.Entity<Media>()
            //    .HasMany<Country>(m => m.Countries)
            //    .WithMany(c => c.Medias)
            //    .Map(mc =>
            //    {
            //        mc.MapLeftKey("SeriesRefId");
            //        mc.MapRightKey("CountryRefId");
            //        mc.ToTable("SeriesCountry");
            //    });
            //modelBuilder.Entity<Media>()
            //    .HasMany<Genre>(m => m.Genres)
            //    .WithMany(c => c.Medias)
            //    .Map(mc =>
            //    {
            //        mc.MapLeftKey("SeriesRefId");
            //        mc.MapRightKey("GenreRefId");
            //        mc.ToTable("SeriesGenre");
            //    });
            modelBuilder.Entity<Media>()
                .HasMany<Season>(sr => sr.Seasons)
                .WithRequired(s => s.CurrentSeries)
                .HasForeignKey<int>(s => s.CurrentSeriesId);

            modelBuilder.Entity<User>()
                .HasMany<Media>(u => u.LikedMedias)
                .WithMany(m => m.UsersLiked)
                .Map(mu =>
                {
                    mu.MapLeftKey("UserRefId");
                    mu.MapRightKey("MediaRefId");
                    mu.ToTable("LikedMedia");
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
                .HasMany<Media>(u => u.WatchedMedias)
                .WithMany(m => m.UsersWatched)
                .Map(mu =>
                {
                    mu.MapLeftKey("UserRefId");
                    mu.MapRightKey("MediaRefId");
                    mu.ToTable("WatchedMedia");
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
        }
    }
}

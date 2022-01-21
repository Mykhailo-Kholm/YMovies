using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;

namespace YMovies.MovieDbService.Repositories.Repository
{
    public class SeasonRepository : IRepository<Season>
    {
        private readonly MoviesContext _context;
        public SeasonRepository(MoviesContext context) => _context = context;
        public IEnumerable<Season> Items => _context.Seasons;

        public Season GetItem(int id)
        {
            var season = _context.Seasons.FirstOrDefault(s => s.SeasonId == id);
            return season;
        }

        public void AddItem(Season item)
        {
            _context.Seasons.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Season item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var season = _context.Seasons.FirstOrDefault(s => s.SeasonId == id);
            if (season == null) return;
            _context.Seasons.Remove(season);
            _context.SaveChanges();
        }

        public void AddSeason(int seriesId)
        {
            var series = _context.Medias.FirstOrDefault(s => s.MediaId == seriesId);
            if (series == null) return;
            var season = new Season()
            {
                Name = "Season" + (series.Seasons.Count + 1),
                CurrentSeries = series,
                CurrentSeriesId = seriesId
            };
            _context.Seasons.Add(season);
            series.Seasons.Add(_context.Seasons.FirstOrDefault(x => x.SeasonId == season.SeasonId));
            _context.SaveChanges();
        }
    }
}

using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;

namespace YMovies.MovieDbService.Repositories.Repository
{
    class SeriesRepository:IRepository<Series>
    {
        private readonly MoviesContext _context;
        public SeriesRepository(MoviesContext context) => _context = context;
        public IEnumerable<Series> Items => _context.Series;
        public Series GetItem(int id)
        {
            var series = _context.Series.FirstOrDefault(s => s.SeriesId == id);
            return series;
        }

        public void AddItem(Series item)
        {
            _context.Series.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Series item)
        {
            _context.Series.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var series = _context.Series.FirstOrDefault(s => s.SeriesId == id);
            if (series == null) return;
            _context.Series.Remove(series);
            _context.SaveChanges();
        }
    }
}

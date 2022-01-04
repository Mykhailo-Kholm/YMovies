using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;

namespace YMovies.MovieDbService.Repositories.Repository
{
    class StatisticRepository:IRepository<Statistic>
    {
        private readonly MoviesContext _context;
        public StatisticRepository(MoviesContext context) => _context = context;
        public IEnumerable<Statistic> Items => _context.Statistics;
        public Statistic GetItem(int id)
        {
            var statistic = _context.Statistics.FirstOrDefault(s => s.StatisticId == id);
            return statistic;
        }

        public void AddItem(Statistic item)
        {
            _context.Statistics.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Statistic item)
        {
            _context.Statistics.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var statistic = _context.Statistics.FirstOrDefault(s => s.StatisticId == id);
            if (statistic == null) return;
            _context.Statistics.Remove(statistic);
            _context.SaveChanges();
        }
    }
}

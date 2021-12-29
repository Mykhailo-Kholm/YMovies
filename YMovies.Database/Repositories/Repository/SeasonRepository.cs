using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YMovies.Database.DatabaseContext;
using YMovies.Database.Models;
using YMovies.Database.Repositories.IRepository;

namespace YMovies.Database.Repositories.Repository
{
    class SeasonRepository:IRepository<Season>
    {
        private readonly MoviesContext _context;
        public SeasonRepository(MoviesContext context)=>_context = context;
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
            _context.Seasons.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var season = _context.Seasons.FirstOrDefault(s => s.SeasonId == id);
            if (season == null) return;
            _context.Seasons.Remove(season);
            _context.SaveChanges();
        }
    }
}

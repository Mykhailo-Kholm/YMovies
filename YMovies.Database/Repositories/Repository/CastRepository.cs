using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using YMovies.Database.DatabaseContext;
using YMovies.Database.Models;
using YMovies.Database.Repositories.IRepository;

namespace YMovies.Database.Repositories.Repository
{
    class CastRepository:IRepository<Cast>
    {
        private readonly MoviesContext _context;
        public CastRepository(MoviesContext context)=> _context = context;
        public IEnumerable<Cast> Items => _context.Cast;

        public Cast GetItem(int id)
        {
            var actor = _context.Cast.FirstOrDefault(a => a.Id == id);
            return actor;
        }

        public void AddItem(Cast item)
        {
            _context.Cast.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Cast item)
        {
            _context.Cast.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var actor = _context.Cast.FirstOrDefault(a => a.Id == id);
            if (actor == null) return;
            _context.Cast.Remove(actor);
            _context.SaveChanges();
        }
    }
}

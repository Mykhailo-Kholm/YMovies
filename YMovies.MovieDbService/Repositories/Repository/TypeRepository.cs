using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Repositories.IRepository;
using Type = YMovies.MovieDbService.Models.Type;

namespace YMovies.MovieDbService.Repositories.Repository
{
    public class TypeRepository:IRepository<Models.Type>
    {
        private readonly MoviesContext _context;
        public TypeRepository(MoviesContext context) => _context = context;
        public IEnumerable<Type> Items => _context.Types;
        public Type GetItem(int id)
        {
            var type = _context.Types.FirstOrDefault(t => t.Id == id);
            return type;
        }

        public void AddItem(Type item)
        {
            _context.Types.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Type item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var type = _context.Types.FirstOrDefault(t => t.Id == id);
            if (type == null) return;
            _context.Types.Remove(type);
            _context.SaveChanges();
        }
    }
}

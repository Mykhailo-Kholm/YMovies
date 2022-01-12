using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;

namespace YMovies.MovieDbService.Repositories.Repository
{
    public class UserRepository : IRepository<User>

    {
        private readonly MoviesContext _context;
        public UserRepository(MoviesContext context) => _context = context;
        public IEnumerable<User> Items => _context.Users;
        public User GetItem(int id)
        {
            var user = _context.Users.Include(u => u.LikedMedias)
                .Include(u => u.WatchedMedias).FirstOrDefault(u => u.Id == id);
            return user;
        }
        public User GetItem(string id)
        {
            var user = _context.Users.Include(u => u.LikedMedias)
                .Include(u => u.WatchedMedias)
                .FirstOrDefault(u => u.IdentityId == id);
            return user;
        }

        public void AddItem(User item)
        {
            if (_context.Users.Any(i => i.IdentityId == item.IdentityId)) return;
            _context.Users.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(User item)
        {
            _context.Users.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null) return;
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}

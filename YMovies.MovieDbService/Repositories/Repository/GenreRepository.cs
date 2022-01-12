using System.Collections.Generic;
using System.Linq;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;

namespace YMovies.MovieDbService.Repositories.Repository
{
    public class GenreRepository : IRepository<Genre>

    {
        private readonly MoviesContext _context;
        public GenreRepository(MoviesContext context) => _context = context;
        public IEnumerable<Genre> Items => _context.Genres;
        public Genre GetItem(int id)
        {
            var genre = _context.Genres.FirstOrDefault(g => g.Id == id);
            return genre;
        }

        public void AddItem(Genre item)
        {
            _context.Genres.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Genre item)
        {
            var temp = _context.Genres.Where(m => m.Id.Equals(item.Id)).FirstOrDefault();
            if (temp == null)
                _context.Genres.Add(item);
            else
            {
                _context.Genres.Remove(temp);
                _context.Genres.Add(item);
            }
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var genre = _context.Genres.FirstOrDefault(g => g.Id == id);
            if (genre == null) return;
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}

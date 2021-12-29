using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using YMovies.Database.DatabaseContext;
using YMovies.Database.Models;
using YMovies.Database.Repositories.IRepository;

namespace YMovies.Database.Repositories.Repository
{
    class MovieRepository:IRepository<Movie>
    {
        private readonly MoviesContext _context;
        public MovieRepository(MoviesContext context) => _context = context;
        public IEnumerable<Movie> Items => _context.Movies;
        public Movie GetItem(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
            return movie;
        }

        public void AddItem(Movie item)
        {
            _context.Movies.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Movie item)
        {
            _context.Movies.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
            if (movie == null) return;
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}

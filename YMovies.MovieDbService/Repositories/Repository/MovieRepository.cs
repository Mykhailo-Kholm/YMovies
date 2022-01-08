using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;

namespace YMovies.MovieDbService.Repositories.Repository
{
    public class MovieRepository:IRepository<Movie>
    {
        private readonly MoviesContext _context;
        public MovieRepository(MoviesContext context) => _context = context;
        public IEnumerable<Movie> Items => _context.Movies
            .Include(m=> m.Countries)
            .Include(m=>m.Genres)
            .Include(m=>m.Cast).Include((m=>m.Type));
        public Movie GetItem(int id)
        {
            var movie = _context.Movies.FirstOrDefault(m => m.MovieId == id);
            return movie;
        }

        public void AddItem(Movie item)
        {
            var cast = new List<Cast>();
            var genres = new List<Genre>();
            var countries = new List<Country>();
            foreach (var actor in item.Cast)
            {
                if (!_context.Cast.Any(i => i.Name==actor.Name&&i.Surname==actor.Surname))
                {
                    _context.Cast.Add(actor);
                    cast.Add(actor);
                    continue;
                }
                cast.Add(_context.Cast.First(i => i.Name==actor.Name&&i.Surname==actor.Surname));
            }
            foreach (var genre in item.Genres)
            {
                if (!_context.Genres.Any(i => i.Name==genre.Name))
                {
                    _context.Genres.Add(genre);
                    genres.Add(genre);
                    continue;
                }
                genres.Add(_context.Genres.First(i => i.Name == genre.Name));
            }
            foreach (var country in item.Countries)
            {
                if (!_context.Countries.Any(i => i.Name == country.Name))
                {
                    _context.Countries.Add(country);
                    countries.Add(country);
                    continue;
                }
                countries.Add(_context.Countries.First(i => i.Name == country.Name));
            }
            item.Cast = cast;
            item.Genres = genres;
            item.Countries = countries;
            _context.Movies.AddOrUpdate(item);
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

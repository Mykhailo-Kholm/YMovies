using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;

namespace YMovies.MovieDbService.Repositories.Repository
{
    class CountryRepository:IRepository<Country>
    {
        private readonly MoviesContext _context;
        public CountryRepository(MoviesContext context) => _context = context;
        public IEnumerable<Country> Items => _context.Countries;

        public Country GetItem(int id)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Id == id);
            return country;
        }

        public void AddItem(Country item)
        {
            _context.Countries.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Country item)
        {
            _context.Countries.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var country = _context.Countries.FirstOrDefault(c => c.Id == id);
            if (country == null) return;
            _context.Countries.Remove(country);
            _context.SaveChanges();
        }
    }
}

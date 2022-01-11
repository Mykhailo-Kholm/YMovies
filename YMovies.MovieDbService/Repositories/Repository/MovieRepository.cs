using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;

namespace YMovies.MovieDbService.Repositories.Repository
{
    public class MovieRepository:ISearchRepository
    {
        private readonly MoviesContext _context;
        public MovieRepository(MoviesContext context) => _context = context;

        public IEnumerable<Media> Items => _context.Medias
            .Include(m => m.Countries)
            .Include(m => m.Genres)
            .Include(m => m.Cast);

        public Media GetItem(int id)
        {
            var movie = _context.Medias.FirstOrDefault(m => m.MediaId == id);

            var list = Items.ToList();

            
            return movie;
        }

        public Media GetItem(string id)
        {
            var movie = _context.Medias.FirstOrDefault(m => m.ImdbId == id);
            return movie;
        }

        public List<Media> GetMostLiked()
        {
            var mediaList = _context.Medias.OrderByDescending(gp => gp.NumberOfLikes)
                                                                                    .Take(100)
                                                                                    .ToList();
            return mediaList;
        }

        public List<Media> GetMediaByTitle(string title)
        {
            var mediaList = _context.Medias.Where(t => t.Title.ToLower().Contains(title.ToLower())).ToList();
            return mediaList;
        }

        public List<Media> GetMediaByParams(string[] genre = null, string[] country = null, string year = null, string type = null)
        {
            var mediaList = _context.Medias.ToList();

            foreach (var gen in genre)
            {
                if (!string.IsNullOrEmpty(gen))
                {
                    mediaList = mediaList.Where(m => m.Genres.Any(g => g.Name.ToLower().Equals(gen.ToLower()))).ToList();
                }
            }

            foreach (var con in country)
            {
                if (!string.IsNullOrEmpty(con))
                {
                    mediaList = mediaList.Where(m => m.Countries.Any(c => c.Name.ToLower().Equals(con.ToLower()))).ToList();
                }
            }
            
            if (!string.IsNullOrEmpty(year))
            {
                mediaList = mediaList.Where(y=> y.Year.Contains(year)).ToList();
            }
            if (!string.IsNullOrEmpty(type))
            {
                mediaList = mediaList.Where(t => t.Type.Name.ToLower().Contains(type.ToLower())).ToList();
            }

            return mediaList;
        }

        public void AddItem(Media item)
        {
            var cast = new List<Cast>();
            var genres = new List<Genre>();
            var countries = new List<Country>();

            if (!_context.Types.Any(t => t.Name == item.Type.Name))
                _context.Types.Add(item.Type);
            else
            {
                var type = _context.Types.First(t => t.Name == item.Type.Name);
                item.Type = type;
            }
            foreach (var actor in item.Cast)
            {
                if (!_context.Cast.Any(i => i.Name==actor.Name))
                {
                    _context.Cast.Add(actor);
                    cast.Add(actor);
                    continue;
                }
                cast.Add(_context.Cast.First(i => i.Name==actor.Name));
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
            _context.Medias.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void UpdateItem(Media item)
        {
            _context.Medias.AddOrUpdate(item);
            _context.SaveChanges();
        }

        public void DeleteItem(int id)
        {
            var movie = _context.Medias.FirstOrDefault(m => m.MediaId == id);
            if (movie == null) return;
            _context.Medias.Remove(movie);
            _context.SaveChanges();
        }

        public List<Media> GetOneHundredMediaRandom()
        {
            var media = _context.Medias.OrderBy(r => System.Guid.NewGuid()).Take(100).ToList();
            return media;
        }
    }
}

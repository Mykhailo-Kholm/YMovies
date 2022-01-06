using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Services.Service;
using IMDbApiLib.Models;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;

namespace YMovies.Web.IMDB.DBWorker
{
    public class DBSeed : ISeed
    {
        private readonly MoviesContext _context;
        private readonly IRepository<Movie> _movieRepository;
        private readonly APIworkerIMDB aPIworkerIMDB;
        public DBSeed()
        {
            _context = new MoviesContext();
            _movieRepository = new MovieRepository(_context);
            aPIworkerIMDB = new APIworkerIMDB();
        }
        public async void AddMovieByImbdId(string imdbId)
        {
            if(_context.Movies.Any(m => m.ImdbId == imdbId))
            {
                return;
            }

            var media = await aPIworkerIMDB.MovieOrSeriesInfo(imdbId);

            if(media.Type is null)
            {
                return;
            }


            _movieRepository.AddItem(MapMovieToDtoFromImdb(media));
        }
        private Movie MapMovieToDtoFromImdb(TitleData imdbModel)
        {
            var movie = new Movie
            {
                ImdbId = imdbModel.Id,
                Title = imdbModel.Title,
                PosterUrl = imdbModel.Image,
                Year = imdbModel.Year,
                Plot = imdbModel.Plot,
                //Budget = 0m,
                BoxOffice = imdbModel.Companies,
                //ImdbRating = Decimal.Parse(imdbModel.IMDbRating),
                //Type = imdbModel.Type,
                Type = new MovieDbService.Models.Type(),
                Cast = new List<Cast>(),
                Genres = new List<Genre>(),
                Countries = new List<Country>(),
                UsersLiked = new List<User>(),
                UsersWatched = new List<User>()
            };

            movie.Type.Name = imdbModel.Type;

            foreach(var actor in imdbModel.ActorList)
            {
                movie.Cast.Add(new Cast
                {
                    Name = actor.Name,
                    PictureUrl = actor.Image
                });
            }

            foreach(var country in imdbModel.CountryList)
            {
                movie.Countries.Add(new Country
                {
                    Name = country.Value
                });
            }

            foreach (var genre in imdbModel.GenreList)
            {
                movie.Genres.Add(new Genre
                {
                    Name = genre.Value
                });
            }

            return movie;
        }

        private SeriesDto MapSeriesToDtoFromImdb(TitleData imdbModel)
        {
            return null;
        }
    }
}
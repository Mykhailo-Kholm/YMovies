using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public async Task AddMovieByImbdId(string imdbId)
        {
            if(_context.Movies.Any(m => m.ImdbId == imdbId))
            {
                return;
            }

            var media = await aPIworkerIMDB.MovieOrSeriesInfo(imdbId);

            if(media.Type == null || media.TvEpisodeInfo != null)
            {
                return;
            }

            _movieRepository.AddItem(MapMovieToDtoFromImdb(media));
            
        }

        public async Task AddMediaByExpression(string expression)
        {
            var movies = await aPIworkerIMDB.SearchMovieAsync(expression);
            var series = await aPIworkerIMDB.SearchSeriesAsync(expression);

            if (movies != null)
            {
                foreach (var movie in movies)
                {
                    await AddMovieByImbdId(movie.Id);
                }
            }

            if (series != null)
            {
                foreach (var serie in series)
                {
                    await AddMovieByImbdId(serie.Id);
                }
            }
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

            movie.Budget = GetDecimal(imdbModel.BoxOffice.Budget);

            movie.ImdbRating = GetDecimal(imdbModel.IMDbRating);

            if (imdbModel.ActorList.Count != 0)
            {
                foreach (var actor in imdbModel.ActorList)
                {
                    movie.Cast.Add(new Cast
                    {
                        Name = actor.Name,
                        PictureUrl = actor.Image
                    });
                }
            }

            if (imdbModel.CountryList.Count != 0)
            {
                foreach (var country in imdbModel.CountryList)
                {
                    movie.Countries.Add(new Country
                    {
                        Name = country.Value
                    });
                }
            }

            if (imdbModel.GenreList.Count != 0)
            {
                foreach (var genre in imdbModel.GenreList)
                {
                    movie.Genres.Add(new Genre
                    {
                        Name = genre.Value
                    });
                }
            }

            return movie;
        }

        private decimal GetDecimal(string budget)
        {

            if (string.IsNullOrEmpty(budget))
            {
                return 0m;
            }

            string pattern = @"\d";

            StringBuilder sb = new StringBuilder();

            foreach (Match m in Regex.Matches(budget, pattern))
            {
                sb.Append(m);
            }
            string number = sb.ToString();
            decimal n = Convert.ToDecimal(number);

            return n;
        }
    }
}